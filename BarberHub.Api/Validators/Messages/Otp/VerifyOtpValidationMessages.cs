using BarberHub.Domain.Constants;

namespace BarberHub.Api.Validators.Messages.Otp;

public static class VerifyOtpValidationMessages
{
    public const string CodeProperty = "Code";
    public const string CodeInvalidFormat = "OTP code format is invalid.";
    public static readonly string CodeValidRegex = $@"^\d{{{OtpConstants.CodeLength}}}$";
}