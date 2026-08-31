namespace BarberHub.Api.Contracts.Auth;

public record TokenResponse(
    string AccessToken,
    DateTimeOffset AccessTokenExpireAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpireAt
);