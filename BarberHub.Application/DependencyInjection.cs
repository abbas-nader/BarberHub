using BarberHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BarberHub.Application;

public static class DependencyInjection
{
    public static void AddApplications(this IServiceCollection services)
    {
        services.AddScoped<BarberService>();
    }
}