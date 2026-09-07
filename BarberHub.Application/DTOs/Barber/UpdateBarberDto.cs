namespace BarberHub.Application.DTOs.Barber;

public record UpdateBarberDto(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string? Description
);