using BarberHub.Application.DTOs.Auth;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.JwtToken;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class AuthenticationService(
    ISalonAdminRepository salonAdminRepository,
    IBarberRepository barberRepository,
    ICustomerRepository customerRepository,
    IPlatformRepository platformAdminRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    ITokenHasher tokenHasher,
    IRefreshTokenRepository refreshTokenRepository)
{
    public async Task<TokenResult> LoginSalonAdminAsync(LoginDto loginDto,
        CancellationToken cancellationToken = default)
    {
        var user = await salonAdminRepository.GetByUserNameAsync(loginDto.Username, cancellationToken)
                   ?? throw new InvalidCredentialsException();
        VerifyPassword(loginDto.Password, user.PasswordHash);

        var claims = new TokenClaims(user.Id, UserRole.SalonAdmin, user.SalonId);
        return await IssueTokenAsync(claims, cancellationToken);
    }

    public async Task<TokenResult> LoginBarberAsync(LoginDto loginDto,
        CancellationToken cancellationToken = default)
    {
        var user = await barberRepository.GetByUserNameAsync(loginDto.Username, cancellationToken)
                   ?? throw new InvalidCredentialsException();
        VerifyPassword(loginDto.Password, user.PasswordHash);

        var claims = new TokenClaims(user.Id, UserRole.Barber, user.SalonId);
        return await IssueTokenAsync(claims, cancellationToken);
    }

    public async Task<TokenResult> LoginCustomerAsync(LoginDto loginDto,
        CancellationToken cancellationToken = default)
    {
        var user = await customerRepository.GetByUserNameAsync(loginDto.Username, cancellationToken)
                   ?? throw new InvalidCredentialsException();
        VerifyPassword(loginDto.Password, user.PasswordHash);

        var claims = new TokenClaims(user.Id, UserRole.Customer, null);
        return await IssueTokenAsync(claims, cancellationToken);
    }

    public async Task<TokenResult> LoginPlatformAdminAsync(LoginDto loginDto,
        CancellationToken cancellationToken = default)
    {
        var user = await platformAdminRepository.GetByUserNameAsync(loginDto.Username, cancellationToken)
                   ?? throw new InvalidCredentialsException();
        VerifyPassword(loginDto.Password, user.PasswordHash);

        var claims = new TokenClaims(user.Id, UserRole.PlatformAdmin, null);
        return await IssueTokenAsync(claims, cancellationToken);
    }

    private void VerifyPassword(string plainPassword, string passwordHash)
    {
        if (!passwordHasher.Verify(plainPassword, passwordHash))
            throw new InvalidCredentialsException();
    }

    private async Task<TokenResult> IssueTokenAsync(TokenClaims claims, CancellationToken cancellationToken)
    {
        var tokenResult = jwtTokenGenerator.Generate(claims);

        var tokenHash = tokenHasher.Hash(tokenResult.RefreshToken);
        var refreshToken = new RefreshToken(tokenHash, claims.UserId, claims.UserRole,
            tokenResult.RefreshTokenExpiresAt);

        await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);

        return tokenResult;
    }

    public async Task<TokenResult> RefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default)
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
        var claims = await BuildClaimsAsync(existingToken.UserId, existingToken.Role, cancellationToken);
        var tokenResult = jwtTokenGenerator.Generate(claims);
        var newTokenHash = tokenHasher.Hash(tokenResult.RefreshToken);
        var newRefreshToken = new RefreshToken(newTokenHash, claims.UserId, existingToken.Role,
            tokenResult.RefreshTokenExpiresAt);
        await refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
        existingToken.MarkReplacedBy(newRefreshToken.Id);
        refreshTokenRepository.Update(existingToken);
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
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

    private async Task<TokenClaims> BuildClaimsAsync(long userId, UserRole role,
        CancellationToken cancellationToken)
    {
        switch (role)
        {
            case UserRole.SalonAdmin:
            {
                var salonAdmin = await salonAdminRepository.GetByIdAsync(userId, cancellationToken)
                                 ?? throw new EntityNotFoundException(nameof(SalonAdmin), userId);
                return new TokenClaims(salonAdmin.Id, UserRole.SalonAdmin, salonAdmin.SalonId);
            }
            case UserRole.Barber:
            {
                var barber = await barberRepository.GetByIdAsync(userId, cancellationToken)
                             ?? throw new EntityNotFoundException(nameof(Barber), userId);
                if (!barber.IsActive)
                    throw new InvalidCredentialsException();
                return new TokenClaims(barber.Id, UserRole.Barber, barber.SalonId);
            }
            case UserRole.Customer:
            {
                var customer = await customerRepository.GetByIdAsync(userId, cancellationToken)
                               ?? throw new EntityNotFoundException(nameof(Customer), userId);
                return new TokenClaims(customer.Id, UserRole.Customer, null);
            }
            case UserRole.PlatformAdmin:
            {
                var platformAdmin = await platformAdminRepository.GetByIdAsync(userId, cancellationToken)
                                    ?? throw new EntityNotFoundException(nameof(PlatformAdmin), userId);
                return new TokenClaims(platformAdmin.Id, UserRole.PlatformAdmin, null);
            }
            default:
                throw new InvalidRefreshTokenException();
        }
    }
}