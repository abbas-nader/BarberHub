namespace BarberHub.Application.Security.JwtToken;

public record TokenResult(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt
    );