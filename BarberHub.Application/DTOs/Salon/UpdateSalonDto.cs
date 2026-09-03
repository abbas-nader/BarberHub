namespace BarberHub.Application.DTOs.Salon;

public record UpdateSalonDto(
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    string? Description,
    long SalonId
);