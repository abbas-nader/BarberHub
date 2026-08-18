using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class WalletTransactionRepository(BarberHubDbContext context)
    : BaseRepository<WalletTransaction>(context), IWalletTransactionRepository
{
}