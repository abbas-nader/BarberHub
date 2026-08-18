using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class ReviewRepository(BarberHubDbContext context) : BaseRepository<Review>(context), IReviewRepository
{
}