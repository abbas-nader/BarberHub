namespace BarberHub.Application.DTOs.Gallery;

public record GalleryDto(
    long Id,
    string? Caption,
    long SalonId,
    long? BarberId,
    long FileId
    );