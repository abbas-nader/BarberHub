namespace BarberHub.Domain.Exceptions;

public class CurrencyMismatchException() : Exception("Cannot operate on money values with different currencies")
{
}