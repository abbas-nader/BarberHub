namespace BarberHub.Application.DTOs.Auth;

public record LoginDto(
    string Username,
    string Password
);