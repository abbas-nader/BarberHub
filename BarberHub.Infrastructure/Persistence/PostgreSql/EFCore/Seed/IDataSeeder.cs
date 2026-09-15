namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Seed;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}