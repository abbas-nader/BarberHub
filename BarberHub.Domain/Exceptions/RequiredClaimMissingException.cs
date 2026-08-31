namespace BarberHub.Domain.Exceptions;

public class RequiredClaimMissingException(string claimType)
    : Exception($"Required claim '{claimType}' was not found on the current user.")
{
}