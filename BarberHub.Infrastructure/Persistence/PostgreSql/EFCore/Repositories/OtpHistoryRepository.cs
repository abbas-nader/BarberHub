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

    public async Task<IReadOnlyList<OtpHistory>> GetAllIssuedAsync(OtpPurpose purpose, long userId, CancellationToken cancellationToken = default)
        => await context.OtpHistories
            .Where(x => x.Purpose == purpose && x.UserId == userId && x.Status == OtpStatus.Issued)
            .ToListAsync(cancellationToken);

    public async Task<OtpHistory?> GetLatestAsync(OtpPurpose purpose, long userId, CancellationToken cancellationToken = default)
        => await context.OtpHistories
            .Where(x => x.Purpose == purpose && x.UserId == userId && x.Status != OtpStatus.SendFailed)
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<int> CountSinceAsync(OtpPurpose purpose, long userId, DateTimeOffset since,
        CancellationToken cancellationToken = default)
        => await context.OtpHistories.CountAsync(
            x => x.Purpose == purpose && x.UserId == userId &&
                 x.Status != OtpStatus.SendFailed && x.CreatedAt >= since,
            cancellationToken);
    public async Task<int> CountSinceByMobileAsync(OtpPurpose purpose, string mobileNumber,
        DateTimeOffset since, CancellationToken cancellationToken = default)
        => await context.OtpHistories
            .Where(x => x.Purpose == purpose
                        && x.User.MobileNumber == mobileNumber
                        && x.Status != OtpStatus.SendFailed
                        && x.CreatedAt >= since)
            .CountAsync(cancellationToken);
    public async Task AddAsync(OtpHistory otpHistory, CancellationToken cancellationToken = default)
        => await context.AddAsync(otpHistory, cancellationToken);

    public void Update(OtpHistory otpHistory)
        => context.Update(otpHistory);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}