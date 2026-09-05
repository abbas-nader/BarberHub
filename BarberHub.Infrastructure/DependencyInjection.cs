using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Infrastructure.Persistence.Mongo;
using BarberHub.Infrastructure.Persistence.Mongo.Repositories;
using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore;
using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;
using BarberHub.Infrastructure.Security.Hash;
using BarberHub.Infrastructure.Security.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<ICustomerRepository, CustomerRepository>();
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
}