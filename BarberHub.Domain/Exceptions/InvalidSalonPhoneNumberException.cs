namespace BarberHub.Domain.Exceptions;

public class InvalidSalonPhoneNumberException() : Exception("Salon phone number cannot be null or empty.")
{
}