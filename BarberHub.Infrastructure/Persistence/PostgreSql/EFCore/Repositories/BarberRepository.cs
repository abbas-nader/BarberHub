using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class BarberRepository(BarberHubDbContext context) : BaseRepository<Barber>(context), IBarberRepository
{
}