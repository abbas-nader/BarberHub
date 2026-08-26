namespace BarberHub.Application.DTOs.SalonAdmin;

public record SalonAdminDto(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber
);