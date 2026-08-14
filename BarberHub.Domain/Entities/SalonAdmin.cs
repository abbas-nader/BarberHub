using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseEntity
{
    public string FullName { get; private set; }
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public long SalonId { get; private set; }

    private SalonAdmin()
    {
    }

    public SalonAdmin(string fullName, string userName, string passwordHash, long salonId, long creationBy)
    {
        ValidateFullName(fullName);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FullName = fullName;
        UserName = userName;
        PasswordHash = passwordHash;
        SalonId = salonId;
        Creation(creationBy);
    }

    private static void ValidateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new RequiredFieldException(nameof(fullName));
        if (fullName is { Length: > SalonAdminConstants.SalonAdminFullNameMaxLength })
            throw new InvalidSalonAdminFullNameException();
    }

    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new RequiredFieldException(nameof(userName));
        if (userName is { Length: > SalonAdminConstants.SalonAdminUserNameMaxLength })
            throw new InvalidSalonAdminUserNameException();
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredFieldException(nameof(passwordHash));
    }
}