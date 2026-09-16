using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.User;

public record CreateUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    string? MobileNumber,
    UserRole Role
);