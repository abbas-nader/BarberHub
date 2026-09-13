namespace BarberHub.Api.Contracts.Gallery;

public record CreateGalleryRequest(
    IFormFile File,
    string OriginFileName,
    string? Caption,
    long? BarberId
);