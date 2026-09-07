using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.Auth;

public record LoginDto(
    string Username,
    string Password,
    UserRole Role
);