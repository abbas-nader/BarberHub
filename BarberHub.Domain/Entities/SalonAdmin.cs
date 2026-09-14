namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseEntity
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    public long SalonId { get; private set; }
    public Salon Salon { get; private set; } = null!;

    private SalonAdmin()
    {
    }

    public SalonAdmin(long userId, long salonId, long creationBy)
    {
        UserId = userId;
        SalonId = salonId;
        Creation(creationBy);
    }
}