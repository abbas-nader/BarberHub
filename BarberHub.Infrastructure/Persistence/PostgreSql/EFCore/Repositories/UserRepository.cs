using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class UserRepository(BarberHubDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.IsDeleted == false,
            cancellationToken);
}