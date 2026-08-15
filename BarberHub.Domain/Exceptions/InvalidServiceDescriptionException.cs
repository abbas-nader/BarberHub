namespace BarberHub.Domain.Exceptions;

public class InvalidServiceDescriptionException()
    : Exception("Service description cannot exceed the maximum allowed length.")
{
}