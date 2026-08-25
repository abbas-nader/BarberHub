namespace BarberHub.Application.DTOs.Salon;

public record UpdateSalonDto(
    long Id,
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    string? Description
);