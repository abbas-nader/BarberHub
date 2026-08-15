namespace BarberHub.Domain.Exceptions;

public class InvalidServiceDurationException()
    : Exception("Invalid service duration. Duration must be greater than zero.")
{
}