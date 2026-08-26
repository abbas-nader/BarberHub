namespace BarberHub.Api.Contracts.SalonAdmin;

public record UpdateSalonAdminRequest(
    long Id,
    string FirstName,
    string LastName,
    string Username,
    string? Password,
    string MobileNumber
);