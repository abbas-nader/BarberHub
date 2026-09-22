namespace BarberHub.Application.Security.Authentication;

public record TokenResult(
    string AccessToken,
    DateTimeOffset AccessTokenExpireAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpireAt
    );