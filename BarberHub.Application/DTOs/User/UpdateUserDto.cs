namespace BarberHub.Application.DTOs.User;

public record UpdateUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string? MobileNumber
);