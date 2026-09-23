using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;

namespace BarberHub.Application.Repositories;

public interface IOtpHistoryRepository
{
    Task<OtpHistory?> GetLatestIssuedAsync(OtpPurpose purpose, long userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OtpHistory>> GetAllIssuedAsync(OtpPurpose purpose, long userId, CancellationToken cancellationToken = default);
    Task<OtpHistory?> GetLatestAsync(OtpPurpose purpose, long userId, CancellationToken cancellationToken = default);
    Task<int> CountSinceAsync(OtpPurpose purpose, long userId, DateTimeOffset since, CancellationToken cancellationToken = default);
    Task AddAsync(OtpHistory otpHistory, CancellationToken cancellationToken = default);
    void Update(OtpHistory otpHistory);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}