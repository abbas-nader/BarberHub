using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Seed;

public class DataSeeder(
    IUserRepository userRepository,
    IPlatformRepository platformAdminRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    ILogger<DataSeeder> logger) : IDataSeeder
{
    private const string DefaultUsername = "abbasnader";
    private const string DefaultPassword = "1234";
    private const string DefaultFirstName = "abbas";
    private const string DefaultLastName = "nader";

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await userRepository.ExistsByUserNameAsync(DefaultUsername, cancellationToken))
        {
            logger.LogInformation("Platform admin already seeded, skipping.");
            return;
        }

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var passwordHash = passwordHasher.Hash(DefaultPassword);
            var user = new User(
                DefaultFirstName,
                DefaultLastName, DefaultUsername, passwordHash, UserRole.PlatformAdmin, null,
                SystemConstants.SystemUserId);
            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);

            var platformAdmin = new PlatformAdmin(user.Id, SystemConstants.SystemUserId);
            await platformAdminRepository.AddAsync(platformAdmin, cancellationToken);
            await platformAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            logger.LogInformation("Platform admin seeded successfully.");
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }
}