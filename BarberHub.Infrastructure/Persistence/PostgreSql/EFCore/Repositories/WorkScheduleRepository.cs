using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class WorkScheduleRepository(BarberHubDbContext context)
    : BaseRepository<WorkSchedule>(context), IWorkScheduleRepository
{
    public async Task<IReadOnlyList<WorkSchedule>> GetAllByBarberIdAsync(long barberId, CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.WorkSchedules
            .Where(x => x.BarberId == barberId && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}