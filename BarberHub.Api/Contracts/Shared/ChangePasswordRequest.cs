namespace BarberHub.Api.Contracts.Shared;

public record ChangePasswordRequest(
    string OldPassword,
    string NewPassword
);