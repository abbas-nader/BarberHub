using Amazon.S3;
using BarberHub.Application.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure.Storage.ArvanCloud;

public class ArvanAvailabilityChecker(
    IAmazonS3 s3Client,
    IOptions<ArvanCloudSetting> options,
    ILogger<ArvanAvailabilityChecker> logger) : IArvanAvailabilityChecker
{
    private readonly ArvanCloudSetting _setting = options.Value;

    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken = default)
    {
        using var timeoutCts =
            new CancellationTokenSource(TimeSpan.FromSeconds(_setting.AvailabilityCheckTimeoutSeconds));
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

        try
        {
            await s3Client.GetBucketLocationAsync(_setting.BucketName, linkedCts.Token);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "ArvanCloud availability check failed.");
            return false;
        }
    }
}