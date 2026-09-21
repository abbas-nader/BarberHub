using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public string? MobileNumber { get; private set; }
    public bool? IsMobileVerified { get; private set; }

    private User()
    {
    }

    public User(string firstName, string lastName, string userName, string passwordHash, UserRole role,
        string? mobileNumber, long creationBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        PasswordHash = passwordHash;
        Role = role;
        if (mobileNumber is not null && Role != UserRole.PlatformAdmin)
        {
            MobileNumber = mobileNumber;
            IsMobileVerified = Role != UserRole.EndUser;
        }

        Creation(creationBy);
    }

    public void Update(string firstName, string lastName, string userName, string? mobileNumber, long modifiedBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        if (mobileNumber is not null)
            MobileNumber = mobileNumber;
        Modified(modifiedBy);
    }

    public void ChangePassword(string passwordHash, long modifiedBy)
    {
        ValidatePasswordHash(passwordHash);
        PasswordHash = passwordHash;
        Modified(modifiedBy);
    }
    public void VerifyMobileNumber(long modifiedBy)
    {
        if (string.IsNullOrWhiteSpace(MobileNumber))
            throw new RequiredFieldException(nameof(MobileNumber));

        if (IsMobileVerified == true)
            throw new MobileNumberAlreadyInUseException();

        IsMobileVerified = true;
        Modified(modifiedBy);
    }
    public void ChangeMobileNumber(string newMobileNumber, long modifiedBy)
    {
        ValidateMobileNumber(newMobileNumber);

        MobileNumber = newMobileNumber;
        IsMobileVerified = true;
        Modified(modifiedBy);
    }
    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new RequiredFieldException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) throw new RequiredFieldException(nameof(lastName));
    }

    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) throw new RequiredFieldException(nameof(userName));
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new RequiredFieldException(nameof(passwordHash));
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber)) throw new RequiredFieldException(nameof(mobileNumber));
    }
}