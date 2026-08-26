using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface ISalonAdminRepository : IRepository<SalonAdmin>
{
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<SalonAdmin>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);
}