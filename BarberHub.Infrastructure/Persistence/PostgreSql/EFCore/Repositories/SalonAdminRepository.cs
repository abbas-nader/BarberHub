using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class SalonAdminRepository(BarberHubDbContext context)
    : BaseRepository<SalonAdmin>(context), ISalonAdminRepository
{
    public async Task<SalonAdmin?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.SalonAdmins.FirstOrDefaultAsync(
            x => x.UserId == userId && !x.IsDeleted, cancellationToken);

    public async Task<IReadOnlyCollection<SalonAdmin>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.SalonAdmins
            .Where(x => x.SalonId == salonId && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}