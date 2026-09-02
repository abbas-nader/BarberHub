namespace BarberHub.Application.DTOs.Service;

public record CreateServiceDto(
    string Name,
    string? Description,
    TimeSpan Duration,
    long SalonId
);