namespace BarberHub.Domain.Constants;

public static class OtpConstants
{
    public const int CodeLength = 6;
    public const int CodeMinValue = 100000;
    public const int CodeMaxValue = 1000000; 
    public const int ExpirationMinutes = 2;
    public const int ResendCooldownSeconds = 120;
    public const int MaxSendsPer24Hours = 5;
    public const int MaxFailedAttempts = 5;
    public const int CodeHashLength = 64;  
}