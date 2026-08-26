namespace BarberHub.Api.Contracts.SalonAdmin;

public record CreateSalonAdminRequest(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string MobileNumber,
    long SalonId
);