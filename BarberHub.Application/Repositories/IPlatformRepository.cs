using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IPlatformRepository : IRepository<PlatformAdmin>
{
    Task<PlatformAdmin?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}