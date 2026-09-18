namespace BarberHub.Application.DTOs.EndUser;

public record EndUserDto(
    long Id,
    string FirstName,
    string LastName,
    string UserName,
    string? MobileNumber,
    bool? IsMobileVerified
);