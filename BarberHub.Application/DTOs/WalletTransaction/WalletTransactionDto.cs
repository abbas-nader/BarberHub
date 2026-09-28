using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.WalletTransaction;

public record WalletTransactionDto(
    long Id,
    decimal AmountValue,
    Currency AmountCurrency,
    TransactionType TransactionType,
    WalletTransactionReason WalletTransactionReason,
    decimal BalanceAfterTransactionValue,
    Currency BalanceAfterTransactionCurrency,
    long? AppointmentId,
    DateTimeOffset CreatedAt
);