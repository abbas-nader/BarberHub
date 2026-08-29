using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class RefreshToken
{
    public long Id { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public long UserId { get; private set; }
    public UserRole Role { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public long? ReplacedByTokenId { get; private set; }

    private RefreshToken()
    {
    }

    public RefreshToken(string tokenHash, long userId, UserRole role, DateTimeOffset expiresAt)
    {
        ValidateTokenHash(tokenHash);
        TokenHash = tokenHash;
        UserId = userId;
        Role = role;
        CreatedAt = DateTimeOffset.UtcNow;
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }

    public void Revoke()
    {
        if (IsRevoked) return;
        IsRevoked = true;
        RevokedAt = DateTimeOffset.UtcNow;
    }

    public void MarkReplacedBy(long newTokenId)
    {
        ReplacedByTokenId = newTokenId;
        Revoke();
    }

    private static void ValidateTokenHash(string tokenHash)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new RequiredFieldException(nameof(tokenHash));
    }
}