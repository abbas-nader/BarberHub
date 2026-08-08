namespace BarberHub.Domain.Exceptions;

public class InvalidSalonCityException() : Exception("Salon city cannot be null or empty.")
{
}