namespace BarberHub.Application.Storage;

public interface IFileStorageService
{
    Task<FileUploadResult> UploadAsync(Stream fileStream, string key, string contentType,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}