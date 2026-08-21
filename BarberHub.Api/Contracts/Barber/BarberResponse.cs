namespace BarberHub.Api.Contracts.Barber;

public record BarberResponse(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber,
    string? Description,
    bool IsActive
    );