using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Constants;

namespace BarberHub.Domain.Entities;

public class Image : BaseEntity
{
    public string ImageUrl { get; private set; } = null!;
    public string? Caption { get; private set; }

    public long SalonId { get; private set; }
    public Salon Salon { get; private set; } = null!;

    public long? BarberId { get; private set; }
    public Barber? Barber { get; private set; }

    private Image()
    {
    }

    public Image(long salonId, string imageUrl, string? caption, long creationBy)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new InvalidImageUrlException();

        SalonId = salonId;
        ImageUrl = imageUrl;
        Caption = caption;
        Creation(creationBy);
    }

    public Image(long barberId, long salonId, string imageUrl, string? caption, long creationBy, bool forBarber)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new InvalidImageUrlException();

        SalonId = salonId;
        BarberId = barberId;
        ImageUrl = imageUrl;
        Caption = caption;
        Creation(creationBy);
    }

    public void UpdateCaption(string? caption, long modifiedBy)
    {
        if (caption is { Length: > ImageConstants.CaptionMaxLength })
            throw new CaptionLengthExceededException();
        Caption = caption;
        Modified(modifiedBy);
    }
}