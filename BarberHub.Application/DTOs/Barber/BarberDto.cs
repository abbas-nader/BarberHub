namespace BarberHub.Application.DTOs.Barber;

public record BarberDto(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber,
    string? Description,
    bool IsActive
);