namespace BarberHub.Application.DTOs.Shared;

public record ChangePasswordDto(
    string OldPassword,
    string NewPassword
);