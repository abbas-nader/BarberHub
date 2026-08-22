namespace BarberHub.Api.Contracts.Barber;

public record UpdateBarberRequest(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string? Password,
    string? Description
);