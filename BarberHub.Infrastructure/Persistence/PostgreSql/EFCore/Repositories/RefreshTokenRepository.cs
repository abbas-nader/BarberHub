using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class RefreshTokenRepository(BarberHubDbContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash,
        CancellationToken cancellationToken = default)
        => await context.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash && x.IsRevoked == false,
            cancellationToken);

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        => await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);

    public void Update(RefreshToken refreshToken)
    => context.RefreshTokens.Update(refreshToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    => context.SaveChangesAsync(cancellationToken);
}