namespace BarberHub.Domain.Exceptions;

public class OtpExpiredException() : Exception("OTP code has expired or was not requested.")
{
}