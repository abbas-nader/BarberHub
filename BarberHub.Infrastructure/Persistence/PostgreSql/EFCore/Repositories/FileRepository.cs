using BarberHub.Application.Repositories;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class FileRepository(BarberHubDbContext context) : BaseRepository<File>(context), IFileRepository
{
}