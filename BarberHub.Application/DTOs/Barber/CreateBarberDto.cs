namespace BarberHub.Application.DTOs.Barber;

public record CreateBarberDto(
    string FirstName,
    string LastName,
    string MobileNumber,
    string UserName,
    string Password,
    string? Description
);