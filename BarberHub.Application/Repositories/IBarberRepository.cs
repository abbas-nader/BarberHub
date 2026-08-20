using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IBarberRepository:IRepository<Barber>
{
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}