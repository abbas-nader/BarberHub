using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IBarberServiceRepository : IRepository<BarberService>
{
    Task<IReadOnlyList<BarberService>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default);
}