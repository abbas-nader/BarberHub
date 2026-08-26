namespace BarberHub.Application.DTOs.SalonAdmin;

public record CreateSalonAdminDto(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string MobileNumber,
    long SalonId
);