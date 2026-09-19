namespace BarberHub.Api.Contracts.PlatformAdmin;

public record PlatformAdminResponse(
    long Id,
    string FirstName,
    string LastName,
    string UserName
);