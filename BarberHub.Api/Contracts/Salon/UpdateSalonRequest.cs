namespace BarberHub.Api.Contracts.Salon;

public record UpdateSalonRequest(
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    string? Description,
    long SalonId
);