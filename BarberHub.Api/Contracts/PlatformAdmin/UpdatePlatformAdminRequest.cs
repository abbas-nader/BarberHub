namespace BarberHub.Api.Contracts.PlatformAdmin;

public record UpdatePlatformAdminRequest(
    string FirstName,
    string LastName,
    string UserName
);