using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<IReadOnlyDictionary<long, User>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken cancellationToken = default);
}