using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class WalletTransaction : BaseEntity
{
    public Money Amount { get; private set; } = null!;
    public TransactionType TransactionType { get; private set; }
    public WalletTransactionReason WalletTransactionReason { get; private set; }
    public Money BalanceAfterTransaction { get; private set; } = null!;

    public long CustomerId { get; private set; }
    public long AppointmentId { get; private set; }

    private WalletTransaction()
    {
    }

    public WalletTransaction(Money amount, TransactionType transactionType,
        WalletTransactionReason walletTransactionReason, Money balanceAfterTransaction, long customerId,
        long appointmentId, long creationBy)
    {
        ValidateAmount(amount);
        Amount = amount;
        TransactionType = transactionType;
        WalletTransactionReason = walletTransactionReason;
        BalanceAfterTransaction = balanceAfterTransaction;
        CustomerId = customerId;
        AppointmentId = appointmentId;
        Creation(creationBy);
    }

    private static void ValidateAmount(Money amount)
    {
        if (amount is null) throw new RequiredFieldException(nameof(amount));
    }
}