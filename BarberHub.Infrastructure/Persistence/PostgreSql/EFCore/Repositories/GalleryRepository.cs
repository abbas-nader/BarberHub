using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class GalleryRepository(BarberHubDbContext context) : BaseRepository<Gallery>(context), IGalleryRepository
{
}