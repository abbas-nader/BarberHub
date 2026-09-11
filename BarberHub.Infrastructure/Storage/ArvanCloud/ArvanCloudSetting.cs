namespace BarberHub.Infrastructure.Storage.ArvanCloud;

public class ArvanCloudSetting
{
    public const string SectionName = "ArvanStorage";

    public string ServiceUrl { get; set; } = "https://s3.ir-thr-at1.arvanstorage.ir";
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = "barberhub-storage";
    public string PublicBaseUrl { get; set; } = "https://barberhub-files.s3.ir-thr-at1.arvanstorage.ir";
    public int AvailabilityCheckTimeoutSeconds { get; set; } = 3;
}