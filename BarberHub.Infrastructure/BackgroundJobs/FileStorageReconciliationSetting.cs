namespace BarberHub.Infrastructure.BackgroundJobs;

public class FileStorageReconciliationSetting
{
    public const string SectionName = "FileStorageReconciliation";
    public int IntervalDays { get; set; } = 3;
}