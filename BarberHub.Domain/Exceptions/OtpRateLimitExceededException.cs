namespace BarberHub.Domain.Exceptions;

public class OtpRateLimitExceededException() : Exception("Too many OTP requests. Please try again later.")
{
}