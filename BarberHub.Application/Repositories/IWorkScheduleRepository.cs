using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IWorkScheduleRepository : IRepository<WorkSchedule>
{
    Task<IReadOnlyList<WorkSchedule>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default);
}