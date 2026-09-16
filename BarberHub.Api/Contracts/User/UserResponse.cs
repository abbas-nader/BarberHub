namespace BarberHub.Api.Contracts.User;

public record UserResponse(
    long Id,
    string FirstName,
    string LastName,
    string UserName,
    string? MobileNumber,
    bool? IsMobileVerified
);