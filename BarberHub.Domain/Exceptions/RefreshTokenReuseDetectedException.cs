namespace BarberHub.Domain.Exceptions;

public class RefreshTokenReuseDetectedException()
    : Exception("Refresh token reuse detected. All related sessions have been revoked.")
{
}