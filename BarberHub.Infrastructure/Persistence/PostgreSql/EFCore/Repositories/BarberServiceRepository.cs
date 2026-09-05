using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class BarberServiceRepository(BarberHubDbContext context)
    : BaseRepository<BarberService>(context), IBarberServiceRepository
{
    public async Task<IReadOnlyList<BarberService>> GetAllByBarberIdAsync(long barberId, CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.BarberServices
            .Where(x => x.BarberId == barberId && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}