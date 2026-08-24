using BarberHub.Domain.Enums;

namespace BarberHub.Domain.Entities;

public class RefreshToken
{
    public long Id { get; set; }
    public string TokenHash { get; set; } = null!;
    public long UserId { get; set; }
    public UserRole Role { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public bool IsRevoked { get; set; }
    public long? ReplacedByTokenId { get; set; }
}