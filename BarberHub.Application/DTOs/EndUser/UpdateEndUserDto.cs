namespace BarberHub.Application.DTOs.EndUser;

public record UpdateEndUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string? MobileNumber
);