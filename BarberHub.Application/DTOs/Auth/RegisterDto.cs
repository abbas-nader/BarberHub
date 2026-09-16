namespace BarberHub.Application.DTOs.Auth;

public record RegisterDto(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string? MobileNumber
);