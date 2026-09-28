using BarberHub.Application.DTOs.WalletTransaction;
using BarberHub.Domain.Enums;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Services.InterFaces;

public interface IWalletTransactionService
{
    Task<Money> GetBalanceAsync(long endUserId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WalletTransactionDto>> GetAllByCurrentUserAsync(
        CancellationToken cancellationToken = default);

    Task<WalletTransactionDto> CreditAsync(long endUserId, Money amount, WalletTransactionReason reason,
        long? appointmentId, long creationBy, CancellationToken cancellationToken = default);

    Task<WalletTransactionDto> DebitAsync(long endUserId, Money amount, WalletTransactionReason reason,
        long? appointmentId, long creationBy, CancellationToken cancellationToken = default);

    Task<WalletTransactionDto> ChargeAsync(decimal amountValue, Currency currency,
        CancellationToken cancellationToken = default);
}