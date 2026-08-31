namespace BarberHub.Domain.Exceptions;

public class InvalidMoneyAmountException() : Exception("Money value cannot be negative.")
{
}