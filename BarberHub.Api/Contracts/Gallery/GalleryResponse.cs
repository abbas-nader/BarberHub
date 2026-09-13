namespace BarberHub.Api.Contracts.Gallery;

public record GalleryResponse(
    long Id,
    string? Caption,
    long SalonId,
    long? BarberId,
    long FileId
);