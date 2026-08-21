namespace BarberHub.Api.Contracts.Barber;

public record CreateBarberRequest(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Username,
    string Password,
    string? Description,
    long SalonId
);