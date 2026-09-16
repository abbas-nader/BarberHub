namespace BarberHub.Application.DTOs.User;

public record UserDto(
    long Id,
    string FirstName,
    string LastName,
    string UserName,
    string? MobileNumber,
    bool? IsMobileVerified,
    string PasswordHash
);