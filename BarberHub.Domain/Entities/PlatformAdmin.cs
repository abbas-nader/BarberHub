namespace BarberHub.Domain.Entities;

public class PlatformAdmin : BaseEntity
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    private PlatformAdmin()
    {
    }

    public PlatformAdmin(long userId, long creationBy)
    {
        UserId = userId;
        Creation(creationBy);
    }
}