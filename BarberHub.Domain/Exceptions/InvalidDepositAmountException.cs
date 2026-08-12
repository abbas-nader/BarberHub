namespace BarberHub.Domain.Exceptions;

public class InvalidDepositAmountException()
    : Exception("Invalid amount of deposit amount. deposit amount must be greater than 0.")
{
}