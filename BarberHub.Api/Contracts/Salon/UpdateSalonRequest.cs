namespace BarberHub.Api.Contracts.Salon;

public record UpdateSalonRequest(
    long Id,
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    string? Description
);