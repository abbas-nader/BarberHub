namespace BarberHub.Application.DTOs;

public record CreateBarberDto(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string Password,
    string? Description,
    long SalonId
);