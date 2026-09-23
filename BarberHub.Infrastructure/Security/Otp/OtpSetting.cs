namespace BarberHub.Infrastructure.Security.Otp;

public class OtpSetting
{
    public const string SectionName = "Otp";
    public string SecretKey { get; set; } = string.Empty;
}