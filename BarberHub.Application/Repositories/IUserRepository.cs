using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}