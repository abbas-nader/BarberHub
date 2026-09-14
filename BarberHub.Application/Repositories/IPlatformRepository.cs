using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IPlatformRepository : IRepository<PlatformAdmin>
{
    Task<PlatformAdmin?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
}