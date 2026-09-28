using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class WalletTransactionRepository(BarberHubDbContext context)
    : BaseRepository<WalletTransaction>(context), IWalletTransactionRepository
{
    public async Task<WalletTransaction?> GetLatestByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.WalletTransactions
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<WalletTransaction>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.WalletTransactions
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}