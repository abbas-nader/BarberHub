using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class SalonRepository(BarberHubDbContext context) : BaseRepository<Salon>(context), ISalonRepository
{
}