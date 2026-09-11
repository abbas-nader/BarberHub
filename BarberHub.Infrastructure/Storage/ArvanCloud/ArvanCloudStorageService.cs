using Amazon.S3;
using Amazon.S3.Model;
using BarberHub.Application.Storage;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions.SharedExceptions;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure.Storage.ArvanCloud;

public class ArvanCloudStorageService(IAmazonS3 s3Client, IOptions<ArvanCloudSetting> options) : IProviderFileStorage
{
    private readonly ArvanCloudSetting _setting = options.Value;
    public StorageProvider Provider => StorageProvider.ArvanCloud;

    public async Task<FileUploadResult> UploadAsync(Stream fileStream, string key, string contentType,
        CancellationToken cancellationToken = default)
    {
        ValidateKey(key);
        ValidateContentType(contentType);
        var request = new PutObjectRequest
        {
            BucketName = _setting.BucketName,
            Key = key,
            InputStream = fileStream,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead
        };
        await s3Client.PutObjectAsync(request, cancellationToken);
        var url = BuilderUrl(key);
        return new FileUploadResult(url, key, StorageProvider.ArvanCloud);
    }

    public async Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken = default)
    {
        var response = await s3Client.GetObjectAsync(_setting.BucketName, key, cancellationToken);
        var memoryStream = new MemoryStream();
        await response.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;
        return memoryStream;
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        ValidateKey(key);
        return s3Client.DeleteObjectAsync(_setting.BucketName, key, cancellationToken);
    }

    private string BuilderUrl(string key)
        => $"{_setting.ServiceUrl.TrimEnd('/')}/{_setting.BucketName}/{key}";

    private static void ValidateKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new RequiredFieldException(nameof(key));
    }

    private static void ValidateContentType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            throw new RequiredFieldException(nameof(contentType));
    }
}