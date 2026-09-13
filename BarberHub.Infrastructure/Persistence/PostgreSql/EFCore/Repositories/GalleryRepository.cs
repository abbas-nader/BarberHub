using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class GalleryRepository(BarberHubDbContext context) : BaseRepository<Gallery>(context), IGalleryRepository
{
    public async Task<IReadOnlyList<Gallery>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
        => await BarberHubDbContext
            .Galleries
            .Where(g => g.SalonId == salonId && g.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}