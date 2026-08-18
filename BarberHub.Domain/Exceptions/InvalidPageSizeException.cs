namespace BarberHub.Domain.Exceptions;

public class InvalidPageSizeException() : Exception("Page size must be greater than or equal to 1.")
{
}