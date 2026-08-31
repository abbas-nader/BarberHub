using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class BarberRepository(BarberHubDbContext context) : BaseRepository<Barber>(context), IBarberRepository
{
    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.Barbers.AnyAsync(x => x.UserName == userName && x.IsDeleted == false,
            cancellationToken);
    }

    public async Task<Barber?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Barbers.FirstOrDefaultAsync(x => x.UserName == userName && x.IsDeleted == false,
            cancellationToken);

    public async Task<IReadOnlyCollection<Barber>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.Barbers
            .Where(x => x.SalonId == salonId && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}