namespace BarberHub.Application.DTOs.SalonAdmin;

public record UpdateSalonAdminDto(
    long Id,
    string FirstName,
    string LastName,
    string Username,
    string? Password,
    string MobileNumber
    );