using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IWalletTransactionRepository : IRepository<WalletTransaction>
{
    Task<WalletTransaction?> GetLatestByUserIdAsync(long userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WalletTransaction>> GetAllByUserIdAsync(long userId,
        CancellationToken cancellationToken = default);
}