using BarberHub.Domain.Enums;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Wallet : BaseEntity
{
    public Money Amount { get; private set; } = null!;
    public TransactionType TransactionType { get; private set; }
    public WalletTransactionReason WalletTransactionReason { get; private set; }
    public Money BalanceAfterTransaction { get; private set; } = null!;
    
    
}