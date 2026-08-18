namespace BarberHub.Domain.Exceptions;

public class InvalidTotalCountException() : Exception("Total count cannot be negative.")
{
}