namespace BarberHub.Domain.Exceptions;

public class InvalidSalonDescriptionException()
    : Exception("Salon description cannot exceed the maximum allowed length.")
{
}