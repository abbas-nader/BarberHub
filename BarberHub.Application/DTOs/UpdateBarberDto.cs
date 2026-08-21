namespace BarberHub.Application.DTOs;

public record UpdateBarberDto(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string? Password,
    string? Description,
    bool IsActive
);