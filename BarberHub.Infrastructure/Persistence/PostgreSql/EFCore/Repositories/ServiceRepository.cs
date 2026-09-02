using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class ServiceRepository(BarberHubDbContext context) : BaseRepository<Service>(context), IServiceRepository
{
    public async Task<IReadOnlyCollection<Service>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Services
            .Where(x => x.SalonId == salonId && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}