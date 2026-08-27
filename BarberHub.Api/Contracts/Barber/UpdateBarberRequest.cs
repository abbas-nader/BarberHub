namespace BarberHub.Api.Contracts.Barber;

public record UpdateBarberRequest(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string? Password,
    string? Description
);