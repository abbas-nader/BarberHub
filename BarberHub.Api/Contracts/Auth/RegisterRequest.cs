namespace BarberHub.Api.Contracts.Auth;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string MobileNumber
);