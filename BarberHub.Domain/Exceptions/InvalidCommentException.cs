namespace BarberHub.Domain.Exceptions;

public class InvalidCommentException() : Exception("Comment cannot be null or empty.")
{
}