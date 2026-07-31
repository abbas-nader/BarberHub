using BarberHub.Domain.Enums;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class WalletTransaction : BaseEntity
{
    public Money Amount { get; private set; } = null!;
    public TransactionType TransactionType { get; private set; }
    public WalletTransactionReason WalletTransactionReason { get; private set; }
    public Money BalanceAfterTransaction { get; private set; } = null!;
    
    public long CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public long AppointmentId  { get; private set; }
    public Appointment Appointment { get; private set; } = null!;

}