using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class SalonAdminRepository(BarberHubDbContext context)
    : BaseRepository<SalonAdmin>(context), ISalonAdminRepository
{
}