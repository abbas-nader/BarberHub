using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IWorkScheduleRepository : IRepository<WorkSchedule>
{
    Task<IReadOnlyList<WorkSchedule>> GetAllByBarberIdsAsync(long barberId,
        CancellationToken cancellationToken = default);
}