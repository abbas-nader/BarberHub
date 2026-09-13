using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IGalleryRepository : IRepository<Gallery>
{
    Task<IReadOnlyList<Gallery>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);
}