using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class PlatformAdminRepository(BarberHubDbContext context)
    : BaseRepository<PlatformAdmin>(context), IPlatformRepository
{
    public async Task<PlatformAdmin?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.PlatformAdmins.FirstOrDefaultAsync(
            x => x.UserId == userId && x.IsDeleted == false, cancellationToken);
}