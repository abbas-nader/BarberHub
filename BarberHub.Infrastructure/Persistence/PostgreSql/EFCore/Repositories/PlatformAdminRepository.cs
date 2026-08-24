using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class PlatformAdminRepository(BarberHubDbContext context)
    : BaseRepository<PlatformAdmin>(context), IPlatformRepository
{
}