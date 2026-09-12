namespace BarberHub.Application.Security.Jwt;

public record TokenResult(
    string AccessToken,
    DateTimeOffset AccessTokenExpireAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpireAt
    );