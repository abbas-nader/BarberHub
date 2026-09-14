using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class BarberRepository(BarberHubDbContext context) : BaseRepository<Barber>(context), IBarberRepository
{
    public async Task<Barber?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Barbers.FirstOrDefaultAsync(
            x => x.UserId == userId && !x.IsDeleted && x.IsActive, cancellationToken);

    public async Task<IReadOnlyList<Barber>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Barbers
            .Where(x => x.SalonId == salonId && !x.IsDeleted)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}