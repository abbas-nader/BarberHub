namespace BarberHub.Api.Contracts.SalonAdmin;

public record SalonAdminResponse(
    long Id,
    string FirstName,
    string LastName,
    string MobileNumber
);