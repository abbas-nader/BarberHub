namespace BarberHub.Domain.Exceptions;

public class InvalidImageUrlException() : Exception("The image URL cannot be null, empty, or invalid.")
{
}