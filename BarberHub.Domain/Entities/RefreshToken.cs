using BarberHub.Domain.Enums;

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
}