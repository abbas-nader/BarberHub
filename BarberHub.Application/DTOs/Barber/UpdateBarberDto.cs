namespace BarberHub.Application.DTOs.Barber;

public record UpdateBarberDto(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string? Password,
    string? Description
);