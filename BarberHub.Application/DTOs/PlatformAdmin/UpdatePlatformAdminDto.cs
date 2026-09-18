namespace BarberHub.Application.DTOs.PlatformAdmin;

public record UpdatePlatformAdminDto(
    string FirstName,
    string LastName,
    string UserName
);