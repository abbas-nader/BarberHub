using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Seed;

public class DataSeeder(
    IUserRepository userRepository,
    IPlatformRepository platformAdminRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IOptions<SeedAdminSetting> options,
    ILogger<DataSeeder> logger) : IDataSeeder
{
    private readonly SeedAdminSetting _setting = options.Value;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await userRepository.ExistsByUserNameAsync(_setting.UserName, cancellationToken))
        {
            logger.LogInformation("Platform admin already seeded, skipping.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_setting.UserName) || string.IsNullOrWhiteSpace(_setting.Password))
            throw new InvalidOperationException(
                $"{SeedAdminSetting.SectionName}:UserName and {SeedAdminSetting.SectionName}:Password must be configured.");

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var passwordHash = passwordHasher.Hash(_setting.Password);
            var user = new User(_setting.FirstName, _setting.LastName, _setting.UserName, passwordHash,
                UserRole.PlatformAdmin, null, SystemConstants.SystemUserId);
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