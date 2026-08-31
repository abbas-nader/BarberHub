namespace BarberHub.Domain.Exceptions;

public class CurrentUserContextUnavailableException()
    : Exception("No HttpContext is available to resolve the current user.")
{
}