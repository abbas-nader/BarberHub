namespace BarberHub.Domain.Exceptions;

public class InvalidFileSizeException()
    : Exception("Invalid file size. Size must be greater than zero.")
{
}