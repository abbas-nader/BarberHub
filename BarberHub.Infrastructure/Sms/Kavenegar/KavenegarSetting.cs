namespace BarberHub.Infrastructure.Sms.Kavenegar;

public class KavenegarSetting
{
    public const string SectionName = "Kavenegar";
    public string ApiKey { get; set; } = string.Empty;
    public string OtpTemplate { get; set; } = "barberhub-verify-mobile";
}