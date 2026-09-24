namespace BarberHub.Api.Constants.Otp;

public static class OtpUriConstants
{
    private const string ControllerName = "otp";
    public const string Send = $"{ControllerName}/send";
    public const string Verify = $"{ControllerName}/verify";
}