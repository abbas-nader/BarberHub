using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class WorkScheduleRepository(BarberHubDbContext context)
    : BaseRepository<WorkSchedule>(context), IWorkScheduleRepository
{
}