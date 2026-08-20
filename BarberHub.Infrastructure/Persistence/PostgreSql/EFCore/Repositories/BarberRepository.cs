using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class BarberRepository(BarberHubDbContext context) : BaseRepository<Barber>(context), IBarberRepository
{
    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.Barbers.AnyAsync(x => x.UserName == userName, cancellationToken: cancellationToken);
    }
}