namespace BarberHub.Application.DTOs.SalonAdmin;

public record UpdateSalonAdminDto(
    string FirstName,
    string LastName,
    string Username,
    string? Password,
    string MobileNumber
    );