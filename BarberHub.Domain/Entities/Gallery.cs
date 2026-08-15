namespace BarberHub.Domain.Entities;

public class Gallery : BaseEntity
{
    public string? Caption { get; private set; }

    public long SalonId { get; private set; }
    public Salon Salon { get; private set; } = null!;

    public long? BarberId { get; private set; }
    public Barber Barber { get; private set; } = null!;

    public long FileId { get; private set; }
    public File File { get; private set; } = null!;

    private Gallery()
    {
    }

    public Gallery(string? caption, long salonId, long? barberId, long fileId, long creationBy)
    {
        Caption = caption;
        SalonId = salonId;
        BarberId = barberId;
        FileId = fileId;
        Creation(creationBy);
    }

    public void Update(string? caption, long modifiedBy)
    {
        Caption = caption;
        Modified(modifiedBy);
    }
}