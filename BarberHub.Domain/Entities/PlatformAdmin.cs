using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class PlatformAdmin : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    private PlatformAdmin()
    {
    }

    public PlatformAdmin(string firstName, string lastName, string userName, string passwordHash, long creationBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        PasswordHash = passwordHash;
        Creation(creationBy);
    }

    public void Update(string firstName, string lastName, string userName, long modifiedBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Modified(modifiedBy);
    }

    public void ChangePassword(string passwordHash,  long modifiedBy)
    {
        ValidatePasswordHash(passwordHash);
        PasswordHash = passwordHash;
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
}