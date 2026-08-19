using BarberHub.Application.Repositories;
using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore;
using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using File = BarberHub.Domain.Entities.File;

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
    }
}