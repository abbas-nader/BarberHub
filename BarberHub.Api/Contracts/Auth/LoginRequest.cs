namespace BarberHub.Api.Contracts.Auth;

public record LoginRequest(
    string Username,
    string Password
);