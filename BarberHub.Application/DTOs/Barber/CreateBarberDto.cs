namespace BarberHub.Application.DTOs.Barber;

public record CreateBarberDto(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string Password,
    string? Description
);