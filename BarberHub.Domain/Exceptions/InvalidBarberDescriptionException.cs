namespace BarberHub.Domain.Exceptions;

public class InvalidBarberDescriptionException()
    : Exception("Barber description cannot exceed the maximum allowed length.")
{
}