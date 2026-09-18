namespace BarberHub.Api.Contracts.EndUser;

public record EndUserResponse(
    long Id,
    string FirstName,
    string LastName,
    string UserName,
    string? MobileNumber,
    bool? IsMobileVerified
);