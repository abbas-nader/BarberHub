using BarberHub.Domain.Enums;

namespace BarberHub.Application.Storage;

public interface IProviderFileStorage
{
    StorageProvider Provider { get; }

     Task<FileUploadResult> UploadAsync(Stream fileStream, string key, string contentType,
        CancellationToken cancellationToken = default);

     Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken = default);
    
     Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}