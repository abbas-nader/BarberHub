namespace BarberHub.Api.Contracts.Auth;

public record RegisterRequset(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string MobileNumber
);