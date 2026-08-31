namespace BarberHub.Domain.Exceptions;

public class UserNotAuthenticatedException() : Exception("The current request is not authenticated.")
{
}