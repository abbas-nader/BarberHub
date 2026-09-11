using BarberHub.Application.Repositories;
using BarberHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class FileRepository(BarberHubDbContext context) : BaseRepository<File>(context), IFileRepository
{
    public async Task<IReadOnlyList<File>> GetAllByStorageProviderAsync(StorageProvider storageProvider,
        CancellationToken cancellationToken = default)
    {
        return await BarberHubDbContext.Files
            .Where(x => x.StorageProvider == storageProvider && x.IsDeleted == false)
            .ToListAsync(cancellationToken);
    }
}