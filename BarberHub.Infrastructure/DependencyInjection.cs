using Amazon.S3;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Storage;
using BarberHub.Infrastructure.BackgroundJobs;
using BarberHub.Infrastructure.Persistence.Mongo;
using BarberHub.Infrastructure.Persistence.Mongo.Repositories;
using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore;
using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;
using BarberHub.Infrastructure.Security.Hash;
using BarberHub.Infrastructure.Security.Jwt;
using BarberHub.Infrastructure.Storage;
using BarberHub.Infrastructure.Storage.ArvanCloud;
using BarberHub.Infrastructure.Storage.Local;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BarberHubDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IBarberRepository, BarberRepository>();
        services.AddScoped<IBarberServiceRepository, BarberServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IGalleryRepository, GalleryRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ISalonRepository, SalonRepository>();
        services.AddScoped<ISalonAdminRepository, SalonAdminRepository>();
        services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
        services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IPlatformRepository, PlatformAdminRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITokenHasher, TokenHasher>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMongo(configuration);
        services.AddJwt(configuration);
        services.AddArvanStorage(configuration);
    }

    private static void AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSetting>(configuration.GetSection(MongoSetting.SectionName));

        MongoMappingConfig.Register();

        services.AddSingleton<MongoContext>();
        services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();
    }

    private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.JwtSettingsSectionName));
    }

    private static void AddArvanStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ArvanCloudSetting>(configuration.GetSection(ArvanCloudSetting.SectionName));
        services.Configure<LocalStorageSetting>(configuration.GetSection(LocalStorageSetting.SectionName));
        services.Configure<FileStorageReconciliationSetting>(
            configuration.GetSection(FileStorageReconciliationSetting.SectionName));
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var arvanOption = sp.GetRequiredService<IOptions<ArvanCloudSetting>>().Value;
            var config = new AmazonS3Config
            {
                ServiceURL = arvanOption.ServiceUrl,
                ForcePathStyle = true,
            };
            return new AmazonS3Client(arvanOption.AccessKey, arvanOption.SecretKey, config);
        });
        services.AddScoped<IFileStorageService, ArvanCloudStorageService>();
        services.AddScoped<IProviderFileStorage, LocalFileStorageService>();
        services.AddScoped<IArvanAvailabilityChecker, ArvanAvailabilityChecker>();
        services.AddScoped<IFileStorageService, ResilientFileStorageService>();

        services.AddHostedService<FileStorageReconciliationJob>();
    }
}