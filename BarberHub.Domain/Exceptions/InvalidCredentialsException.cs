namespace BarberHub.Domain.Exceptions;

public class InvalidCredentialsException() : Exception("Refresh token is invalid or expired.")
{
}