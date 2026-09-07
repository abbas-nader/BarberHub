using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.Auth;

public record LoginRequest(
    string Username,
    string Password,
    UserRole Role
);