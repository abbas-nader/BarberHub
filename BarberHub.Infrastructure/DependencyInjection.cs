using BarberHub.Infrastructure.Persistence.PostgreSql.EFCore;
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
    }
}