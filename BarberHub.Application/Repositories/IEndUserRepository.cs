using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IEndUserRepository : IRepository<EndUser>
{
    Task<EndUser?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
}