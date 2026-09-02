using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IServiceRepository : IRepository<Service>
{
    Task<IReadOnlyCollection<Service>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);
}