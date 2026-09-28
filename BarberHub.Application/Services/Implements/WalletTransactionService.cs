using BarberHub.Application.DTOs.WalletTransaction;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Authentication;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Services.Implements;

public class WalletTransactionService(IWalletTransactionRepository walletTransactionRepository,
    ICurrentUserService currentUserService, IEndUserRepository endUserRepository):  IWalletTransactionService
{
    public async Task<Money> GetBalanceAsync(long endUserId, CancellationToken cancellationToken = default)
    {
        var lastTransaction = await walletTransactionRepository.GetLatestByUserIdAsync(endUserId, cancellationToken);
        return lastTransaction?.BalanceAfterTransaction ?? new Money(0, Currency.Irr);
    }

    public async Task<IReadOnlyList<WalletTransactionDto>> GetAllByCurrentUserAsync(
        CancellationToken cancellationToken = default)
    {
        var endUser = await endUserRepository.GetByUserIdAsync(currentUserService.CurrentUser.UserId,
                          cancellationToken) ??
                      throw new EntityNotFoundException(nameof(EndUser), currentUserService.CurrentUser.UserId);

        var transactions = await walletTransactionRepository.GetAllByUserIdAsync(endUser.Id, cancellationToken);
        return transactions.Select(ToDto).ToList();
    }

    public async Task<WalletTransactionDto> CreditAsync(long endUserId, Money amount, WalletTransactionReason reason, long? appointmentId, long creationBy,
        CancellationToken cancellationToken = default)
    {
        var currentBalance = await GetBalanceAsync(endUserId, cancellationToken);
        var newBalance = currentBalance + amount;

        var transaction = new WalletTransaction(amount, TransactionType.Credit, reason,
            newBalance, endUserId, appointmentId, creationBy);

        await walletTransactionRepository.AddAsync(transaction, cancellationToken);
        await walletTransactionRepository.SaveChangesAsync(cancellationToken);
        return ToDto(transaction);
    }

    public async Task<WalletTransactionDto> DebitAsync(long endUserId, Money amount, WalletTransactionReason reason, long? appointmentId, long creationBy,
        CancellationToken cancellationToken = default)
    {
        var currentBalance = await GetBalanceAsync(endUserId, cancellationToken);
        var newBalance = currentBalance - amount; 

        var transaction = new WalletTransaction(amount, TransactionType.Debit, reason,
            newBalance, endUserId, appointmentId, creationBy);

        await walletTransactionRepository.AddAsync(transaction, cancellationToken);
        await walletTransactionRepository.SaveChangesAsync(cancellationToken);
        return ToDto(transaction);
    }

    public async Task<WalletTransactionDto> ChargeAsync(decimal amountValue, Currency currency, CancellationToken cancellationToken = default)
    {
        var endUser = await endUserRepository.GetByUserIdAsync(currentUserService.CurrentUser.UserId,
                          cancellationToken) ??
                      throw new EntityNotFoundException(nameof(EndUser), currentUserService.CurrentUser.UserId);

        // TODO: add payment gateway
        var amount = new Money(amountValue, currency);

        return await CreditAsync(endUser.Id, amount, WalletTransactionReason.WalletTopUp, null,
            currentUserService.CurrentUser.UserId, cancellationToken);    }

    private static WalletTransactionDto ToDto(WalletTransaction walletTransaction)
        => new(
            walletTransaction.Id,
            walletTransaction.Amount.Value,
            walletTransaction.Amount.Currency,
            walletTransaction.TransactionType,
            walletTransaction.WalletTransactionReason,
            walletTransaction.BalanceAfterTransaction.Value,
            walletTransaction.BalanceAfterTransaction.Currency,
            walletTransaction.AppointmentId,
            walletTransaction.CreatedAt
        );
}