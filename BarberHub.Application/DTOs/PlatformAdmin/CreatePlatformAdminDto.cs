namespace BarberHub.Application.DTOs.PlatformAdmin;

public record CreatePlatformAdminDto(
    string FirstName,
    string LastName,
    string UserName,
    string Password
);