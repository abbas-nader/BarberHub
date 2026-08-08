namespace BarberHub.Domain.Exceptions;

public class InvalidSalonAddressException() : Exception("Salon address cannot be null or empty.")
{
}