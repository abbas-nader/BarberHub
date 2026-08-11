namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public long SalonId { get; private set; }

    private SalonAdmin()
    {
    }

    public SalonAdmin(string fullName, string userName, string passwordHash, long salonId, long creationBy)
    {
        ValidateFullName(fullName);
        
        FullName = fullName;
        UserName = userName;
        PasswordHash = passwordHash;
        SalonId = salonId;
        Creation(creationBy);
    }
    private static void ValidateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName) || fullName is { Length: > 100 })
            throw new ();
    }
    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName) || userName is { Length: > 100 })
            throw new ();
    }
}