using BarberHub.Domain.Exceptions;

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

    private Image(string imageUrl, string? caption, long salonId, long? barberId = null)
    {
        ImageUrl = imageUrl;
        Caption = caption;
        SalonId = salonId;
        BarberId = barberId;
    }

    private static string CheckImageUrl(string imageUrl)
    {
        return string.IsNullOrWhiteSpace(imageUrl) ? throw new InvalidImageUrlException() : imageUrl;
    }

    public static Image CreateForSalon(long salonId, string imageUrl, string? caption, long creationBy)
    {
        var url = CheckImageUrl(imageUrl);
        var image = new Image(url, caption, salonId);
        image.SetCreationInfo(creationBy);
        return image;
    }

    public static Image CreateForBarber(long barberId, long salonId, string imageUrl, string? caption, long creationBy)
    {
        var url = CheckImageUrl(imageUrl);
        var image = new Image(url, caption, salonId, barberId);
        image.SetCreationInfo(creationBy);
        return image;
    }

    public void UpdateCaption(string? caption, long modifiedBy)
    {
        if (caption is { Length: > 500 })
            throw new CaptionLengthExceededException();
        Caption = caption;
        MarkAsModified(modifiedBy);
    }
}