using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class ServiceRepository(BarberHubDbContext context) : BaseRepository<Service>(context), IServiceRepository
{
}