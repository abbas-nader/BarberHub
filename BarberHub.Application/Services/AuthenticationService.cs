using BarberHub.Application.DTOs.Auth;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class AuthenticationService(
    ISalonAdminRepository salonAdminRepository,
    IBarberRepository barberRepository,
    IUserRepository userRepository,
    IPlatformRepository platformAdminRepository,
    ISalonRepository salonRepository,
    IPasswordHasher passwordHasher,
    IJwtGenerator jwtGenerator,
    ITokenHasher tokenHasher,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<TokenResult> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByUserNameAsync(loginDto.Username, cancellationToken)
                   ?? throw new InvalidCredentialsException();
        VerifyPassword(loginDto.Password, user.PasswordHash);
        if (user.Role != loginDto.Role)
            throw new InvalidCredentialsException();

        var claims = await BuildClaimsAsync(user, cancellationToken);
        return await IssueTokenAsync(claims, cancellationToken);
    }

    private void VerifyPassword(string plainPassword, string passwordHash)
    {
        if (!passwordHasher.Verify(plainPassword, passwordHash))
            throw new InvalidCredentialsException();
    }

    private async Task<TokenResult> IssueTokenAsync(TokenClaims claims, CancellationToken cancellationToken)
    {
        var tokenResult = jwtGenerator.Generate(claims);
        var tokenHash = tokenHasher.Hash(tokenResult.RefreshToken);
        var refreshToken = new RefreshToken(tokenHash, claims.UserId, claims.UserRole,
            tokenResult.RefreshTokenExpireAt);
        await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
        return tokenResult;
    }

    public async Task<TokenResult> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHash = tokenHasher.Hash(refreshToken);
        var existingToken = await refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken) ??
                            throw new InvalidRefreshTokenException();
        if (existingToken.IsRevoked)
        {
            await RevokeChainAsync(existingToken, cancellationToken);
            throw new RefreshTokenReuseDetectedException();
        }
        if (existingToken.ExpiresAt < DateTimeOffset.UtcNow)
            throw new InvalidRefreshTokenException();

        var user = await userRepository.GetByIdAsync(existingToken.UserId, cancellationToken)
                   ?? throw new InvalidRefreshTokenException();
        var claims = await BuildClaimsAsync(user, cancellationToken);
        var tokenResult = jwtGenerator.Generate(claims);
        var newTokenHash = tokenHasher.Hash(tokenResult.RefreshToken);
        var newRefreshToken = new RefreshToken(newTokenHash, claims.UserId, existingToken.Role,
            tokenResult.RefreshTokenExpireAt);

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            await refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
            await refreshTokenRepository.SaveChangesAsync(cancellationToken);
            existingToken.MarkReplacedBy(newRefreshToken.Id);
            refreshTokenRepository.Update(existingToken);
            await refreshTokenRepository.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransaction(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
        return tokenResult;
    }

    public async Task RevokeAsync(string rawRefreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHash = tokenHasher.Hash(rawRefreshToken);
        var existingToken = await refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);
        if (existingToken is null) return;
        existingToken.Revoke();
        refreshTokenRepository.Update(existingToken);
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task RevokeChainAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        var current = token;
        while (current.ReplacedByTokenId is not null)
        {
            var next = await refreshTokenRepository.GetByIdAsync(current.ReplacedByTokenId.Value, cancellationToken);
            if (next is null) break;
            next.Revoke();
            refreshTokenRepository.Update(next);
            current = next;
        }
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<TokenClaims> BuildClaimsAsync(User user, CancellationToken cancellationToken)
    {
        switch (user.Role)
        {
            case UserRole.SalonAdmin:
            {
                var salonAdmin = await salonAdminRepository.GetByUserIdAsync(user.Id, cancellationToken)
                                 ?? throw new InvalidCredentialsException();
                var salon = await salonRepository.GetByIdAsync(salonAdmin.SalonId, cancellationToken);
                if (salon is null || !salon.IsActive) throw new InvalidCredentialsException();
                return new TokenClaims(user.Id, UserRole.SalonAdmin, salonAdmin.SalonId);
            }
            case UserRole.Barber:
            {
                var barber = await barberRepository.GetByUserIdAsync(user.Id, cancellationToken)
                             ?? throw new InvalidCredentialsException();
                if (!barber.IsActive) throw new InvalidCredentialsException();
                var salon = await salonRepository.GetByIdAsync(barber.SalonId, cancellationToken);
                if (salon is null || !salon.IsActive) throw new InvalidCredentialsException();
                return new TokenClaims(user.Id, UserRole.Barber, barber.SalonId);
            }
            case UserRole.EndUser:
                return new TokenClaims(user.Id, UserRole.EndUser, null);
            case UserRole.PlatformAdmin:
                return new TokenClaims(user.Id, UserRole.PlatformAdmin, null);
            default:
                throw new InvalidCredentialsException();
        }
    }
}