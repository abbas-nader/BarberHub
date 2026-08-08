namespace BarberHub.Domain.Exceptions;

public class InvalidSalonNameException() : Exception("Salon name cannot be null or empty.")
{
}