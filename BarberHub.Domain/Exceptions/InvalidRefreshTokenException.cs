namespace BarberHub.Domain.Exceptions;

public class InvalidRefreshTokenException() : Exception("Refresh token is invalid or expired.")
{
}