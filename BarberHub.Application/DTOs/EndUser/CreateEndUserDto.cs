namespace BarberHub.Application.DTOs.EndUser;

public record CreateEndUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    string? MobileNumber
);