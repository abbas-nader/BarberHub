namespace BarberHub.Application.DTOs.Service;

public record UpdateServiceDto(
    long Id,
    string Name,
    string? Description,
    TimeSpan Duration
);