using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;

    public long SalonId { get; private set; }
    public Salon Salon { get; private set; } = null!;

    private SalonAdmin()
    {
    }

    public SalonAdmin(string firstName, string lastName, string userName, string passwordHash, string mobileNumber,
        long salonId, long creationBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        ValidateMobileNumber(mobileNumber);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        PasswordHash = passwordHash;
        MobileNumber = mobileNumber;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void Update(string firstName, string lastName, string userName, string passwordHash, string mobileNumber,
        long modifiedBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        ValidateMobileNumber(mobileNumber);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        PasswordHash = passwordHash;
        MobileNumber = mobileNumber;
        Modified(modifiedBy);
    }


    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new RequiredFieldException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new RequiredFieldException(nameof(lastName));
    }

    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new RequiredFieldException(nameof(userName));
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredFieldException(nameof(passwordHash));
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new RequiredFieldException(nameof(mobileNumber));
    }
}