namespace BarberHub.Domain.Exceptions;

public class InvalidPageNumberException() : Exception("Page number must be greater than or equal to 1.")
{
}