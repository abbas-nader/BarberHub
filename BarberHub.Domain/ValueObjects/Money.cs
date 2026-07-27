using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Domain.ValueObjects;

public sealed record Money
{
    public decimal Value { get; }
    public Currency Currency { get; }

    public Money(decimal value, Currency currency)
    {
        if (value < 0) throw new InvalidDepositAmountException();
        Value = value;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Value + other.Value, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return Value < other.Value ? throw new InsufficientMoneyException() : new Money(Value - other.Value, Currency);
    }

    public void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new CurrencyMismatchException();
    }

    public static Money operator +(Money left, Money right) => left.Add(right);

    public static Money operator -(Money left, Money right) => left.Subtract(right);
}