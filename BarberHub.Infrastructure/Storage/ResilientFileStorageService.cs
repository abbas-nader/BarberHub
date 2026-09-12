using BarberHub.Application.Storage;
using BarberHub.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BarberHub.Infrastructure.Storage;

public class ResilientFileStorageService : IFileStorageService
{
    private readonly IReadOnlyDictionary<StorageProvider, IProviderFileStorage> _providers;
    private readonly IArvanAvailabilityChecker _availabilityChecker;
    private ILogger<ResilientFileStorageService> _logger;

    public ResilientFileStorageService(IEnumerable<IProviderFileStorage> providers,
        IArvanAvailabilityChecker availabilityChecker, ILogger<ResilientFileStorageService> logger)
    {
        _providers = providers.ToDictionary(x => x.Provider);
        _availabilityChecker = availabilityChecker;
        _logger = logger;
    }

    public async Task<FileUploadResult> UploadAsync(Stream fileStream, string key, string contentType,
        CancellationToken cancellationToken = default)
    {
        if (await _availabilityChecker.IsAvailableAsync(cancellationToken))
        {
            try
            {
                return await _providers[StorageProvider.ArvanCloud]
                    .UploadAsync(fileStream, key, contentType, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Upload to ArvanCloud failed for key {Key}. Felling back to local storage",
                    key);
            }
        }
        else
        {
            _logger.LogWarning("ArvanCloud is unavailable. Falling back to local storage for  key {Key}", key);
        }

        if (fileStream.CanSeek)
            fileStream.Position = 0;
        return await _providers[StorageProvider.Route]
            .UploadAsync(fileStream,
                key,
                contentType,
                cancellationToken);
    }

    public Task<Stream> DownloadAsync(string key, StorageProvider storageProvider,
        CancellationToken cancellationToken = default)
        => _providers[storageProvider].DownloadAsync(key, cancellationToken);

    public Task DeleteAsync(string key, StorageProvider storageProvider,
        CancellationToken cancellationToken = default)
        => _providers[storageProvider].DeleteAsync(key, cancellationToken);
}