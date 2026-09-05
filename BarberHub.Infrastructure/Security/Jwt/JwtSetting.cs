namespace BarberHub.Infrastructure.Security.Jwt;

public class JwtSetting
{
    public const string JwtSettingsSectionName = "Jwt";

    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = "BarberHub";
    public string Audience { get; set; } = "BarberHubClient";
    public int AccessTokenExpirationMinutes { get; set; } = 15;
    public int RefreshTokenExpirationDays { get; set; } = 7;
}