using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class EndUserRepository(BarberHubDbContext context) : BaseRepository<EndUser>(context), IEndUserRepository
{
    public async Task<EndUser?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.EndUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.IsDeleted == false,
            cancellationToken);
}