namespace BarberHub.Infrastructure.Security.JwtToken;

public class JwtSetting
{
    public const string JwtSettingsSectionName = "Jwt";

    public string SecretKey { get; set; } =
        "BDxZfOsOV0QSRR/qcTxh5BY1uEcJ6G5lWzNSSlTsr0Yk0KN6vtxIBbZleml2g0YQ1DpfpWL/aVN4k+DglwBVHw==";

    public string Issuer { get; set; } = "BarberHub";
    public string Audience { get; set; } = "BarberHubClient";
    public int AccessTokenExpirationMinutes { get; set; } = 15;
    public int RefreshTokenExpirationMinutes { get; set; } = 7;
}