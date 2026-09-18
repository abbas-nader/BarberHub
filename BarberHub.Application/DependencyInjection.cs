using BarberHub.Application.Services;
using BarberHub.Application.Services.Implements;
using BarberHub.Application.Services.InterFaces;
using Microsoft.Extensions.DependencyInjection;

namespace BarberHub.Application;

public static class DependencyInjection
{
    public static void AddApplications(this IServiceCollection services)
    {
        services.AddScoped<IBarberService, BarberService>();
        services.AddScoped<IExceptionLogService, ExceptionLogService>();
        services.AddScoped<ISalonService, SalonService>();
        services.AddScoped<ISalonAdminService, SalonAdminService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IServiceCatalogService, ServiceCatalogService>();
        services.AddScoped<IBarberServiceCatalogService, BarberServiceCatalogService>();
        services.AddScoped<IWorkScheduleService, WorkScheduleService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IGalleryService, GalleryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEndUserService, EndUserService>();
    }
}