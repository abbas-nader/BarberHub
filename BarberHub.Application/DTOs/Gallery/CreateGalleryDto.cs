namespace BarberHub.Application.DTOs.Gallery;

public record CreateGalleryDto(
    Stream FileStream,
    string OriginFileName,
    string ContentType,
    long Size,
    string? Caption,
    long? BarberId
);