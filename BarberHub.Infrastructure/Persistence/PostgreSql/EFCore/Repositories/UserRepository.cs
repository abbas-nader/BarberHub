using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class UserRepository(BarberHubDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.IsDeleted == false,
            cancellationToken);

    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Users.AnyAsync(x => x.UserName == userName && x.IsDeleted == false,
            cancellationToken);

    public async Task<IReadOnlyDictionary<long, User>> GetByIdsAsync(IEnumerable<long> ids,
        CancellationToken cancellationToken = default)
    {
        var idList = ids.Distinct().ToList();
        var users = await BarberHubDbContext.Users
            .Where(x => idList.Contains(x.Id) && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return users.ToDictionary(x => x.Id);
    }
}