using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IBarberRepository : IRepository<Barber>
{
    Task<Barber?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Barber>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);
}