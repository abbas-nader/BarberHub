namespace BarberHub.Application.DTOs.PlatformAdmin;

public record PlatformAdminDto(
    long Id,
    string FirstName,
    string LastName,
    string UserName
);