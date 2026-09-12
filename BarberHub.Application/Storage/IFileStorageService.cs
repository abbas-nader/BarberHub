using BarberHub.Domain.Enums;

namespace BarberHub.Application.Storage;

public interface IFileStorageService
{
    Task<FileUploadResult> UploadAsync(Stream fileStream, string key, string contentType,
        CancellationToken cancellationToken = default);

    Task<Stream> DownloadAsync(string key, StorageProvider storageProvider,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string key, StorageProvider storageProvider,
        CancellationToken cancellationToken = default);
}