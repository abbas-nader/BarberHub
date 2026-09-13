namespace BarberHub.Application.DTOs.Gallery;

public record CreateGalleryDto(
    string? Caption,
    long? BarberId,
    long FileId
);