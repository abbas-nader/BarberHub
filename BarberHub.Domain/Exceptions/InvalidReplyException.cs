namespace BarberHub.Domain.Exceptions;

public class InvalidReplyException() : Exception("Reply cannot be null or empty.")
{
}