namespace BarberHub.Api.Contracts.PlatformAdmin;

public record CreatePlatformAdminRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Password
);