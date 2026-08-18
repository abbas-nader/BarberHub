using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class BarberServiceRepository(BarberHubDbContext context)
    : BaseRepository<BarberService>(context), IBarberServiceRepository
{
}