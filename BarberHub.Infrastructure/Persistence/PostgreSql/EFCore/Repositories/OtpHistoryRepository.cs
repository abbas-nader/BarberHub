using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class OtpHistoryRepository(BarberHubDbContext context) : IOtpHistoryRepository
{
    public async Task<OtpHistory?> GetLatestIssuedAsync(OtpPurpose purpose, long userId,
        CancellationToken cancellationToken = default)
        => await context.OtpHistories.Where(x => x.Purpose == purpose &&
                                                 x.UserId == userId &&
                                                 x.Status == OtpStatus.Issued)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task AddAsync(OtpHistory otpHistory, CancellationToken cancellationToken = default)
        => await context.AddAsync(otpHistory, cancellationToken);

    public void Update(OtpHistory otpHistory)
        => context.Update(otpHistory);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}