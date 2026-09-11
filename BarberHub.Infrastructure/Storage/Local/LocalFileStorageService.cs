using BarberHub.Application.Storage;
using BarberHub.Domain.Enums;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure.Storage.Local;

public class LocalFileStorageService(IOptions<LocalStorageSetting> options) : IProviderFileStorage
{
    private readonly LocalStorageSetting _settings = options.Value;
    public StorageProvider Provider => StorageProvider.Route;
    public async Task<FileUploadResult> UploadAsync(Stream fileStream, string key, string contentType, CancellationToken cancellationToken = default)
    {
        var fullPath = GetFullPath(key);
        var directory = Path.GetDirectoryName(fullPath);
        if (directory != null) Directory.CreateDirectory(directory);

        await using var destination = File.Create(fullPath);
        await fileStream.CopyToAsync(destination, cancellationToken);

        var url = $"{_settings.PublicUrl.TrimEnd('/')}/{key}";
        return new FileUploadResult(url, key, StorageProvider.Route);
    }

    public Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken = default)
    {
        Stream stream = File.OpenRead(GetFullPath(key));
        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        var fullPath  = GetFullPath(key);
        if(File.Exists(fullPath))
            File.Delete(fullPath);
        return Task.CompletedTask;
    }
    private string GetFullPath(string key) => Path.Combine(_settings.RootPath , key);
}