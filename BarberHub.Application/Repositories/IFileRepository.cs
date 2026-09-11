using BarberHub.Domain.Enums;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Application.Repositories;

public interface IFileRepository : IRepository<File>
{
    Task<IReadOnlyList<File>> GetAllByStorageProviderAsync(StorageProvider storageProvider,
        CancellationToken cancellationToken = default);
}