namespace BarberHub.Application.DTOs.Service;

public record ServiceDto(
    long Id,
    string Name,
    string? Description,
    TimeSpan Duration
);