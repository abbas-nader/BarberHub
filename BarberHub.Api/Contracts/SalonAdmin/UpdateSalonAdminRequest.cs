namespace BarberHub.Api.Contracts.SalonAdmin;

public record UpdateSalonAdminRequest(
    string FirstName,
    string LastName,
    string Username,
    string? Password,
    string MobileNumber
);