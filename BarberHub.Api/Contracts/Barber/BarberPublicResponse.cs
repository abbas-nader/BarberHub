namespace BarberHub.Api.Contracts.Barber;

public record BarberPublicResponse(
    long Id,
    string FirstName,
    string LastName,
    string? Description,
    bool IsActive
);