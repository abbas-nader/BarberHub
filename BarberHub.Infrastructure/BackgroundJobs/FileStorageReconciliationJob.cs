using BarberHub.Application.Repositories;
using BarberHub.Application.Storage;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Infrastructure.BackgroundJobs;

public class FileStorageReconciliationJob(
    IServiceScopeFactory scopeFactory,
    IOptions<FileStorageReconciliationSetting> options,
    ILogger<FileStorageReconciliationJob> logger) : BackgroundService
{
    private readonly FileStorageReconciliationSetting _setting = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = TimeSpan.FromDays(Math.Max(_setting.IntervalDays, 1));
        using var timer = new PeriodicTimer(interval);
        do
        {
            try
            {
                await ReconcileAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "File storage reconciliation job failed.");
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task ReconcileAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var fileRepository = scope.ServiceProvider.GetRequiredService<IFileRepository>();
        var availabilityChecker = scope.ServiceProvider.GetRequiredService<IArvanAvailabilityChecker>();
        var providers = scope.ServiceProvider.GetServices<IProviderFileStorage>().ToDictionary(x => x.Provider);

        if (!await availabilityChecker.IsAvailableAsync(cancellationToken))
        {
            logger.LogInformation("ArvanCloud is unavailable, skipping this reconciliation cycle.");
            return;
        }

        var localFiles = await fileRepository.GetAllByStorageProviderAsync(StorageProvider.Route, cancellationToken);
        if (localFiles.Count == 0)
            return;
        var arvanStorage = providers[StorageProvider.ArvanCloud];
        var localStorage = providers[StorageProvider.Route];

        foreach (var file in localFiles)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await MigrateFileAsync(file, localStorage, arvanStorage, fileRepository, cancellationToken);
        }
    }

    private async Task MigrateFileAsync(File file, IProviderFileStorage localStorage, IProviderFileStorage arvanStorage,
        IFileRepository fileRepository, CancellationToken cancellationToken)
    {
        try
        {
            await using var stream = await localStorage.DownloadAsync(file.StorageKey, cancellationToken);
            var uploadResult =
                await arvanStorage.UploadAsync(stream, file.StorageKey, file.ContentType, cancellationToken);
            file.ChangeStorageProvider(uploadResult.Url, uploadResult.Key, StorageProvider.ArvanCloud,
                SystemConstants.SystemUserId);
            await fileRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to migrate file {FileId} to ArvanCloud. Will retry next cycle.", file.Id);
            return;
        }

        try
        {
            await localStorage.DeleteAsync(file.StorageKey, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex,
                "File {FileId} migrated to ArvanCloud but local copy could not be removed.", file.Id);
        }
    }
}