using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class OtpHistory
{
    public long Id { get; private set; }
    public OtpPurpose Purpose { get; private set; }
    public string CodeHash { get; private set; } = null!;
    public int FailedAttempts { get; private set; }
    public OtpStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? ResolvedAt { get; private set; }

    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    private OtpHistory()
    {
    }

    public OtpHistory(OtpPurpose purpose, string codeHash, DateTimeOffset expiresAt, long userId)
    {
        if (string.IsNullOrWhiteSpace(codeHash))
            throw new RequiredFieldException(nameof(codeHash));

        Purpose = purpose;
        CodeHash = codeHash;
        FailedAttempts = 0;
        Status = OtpStatus.Issued;
        CreatedAt = DateTimeOffset.UtcNow;
        ExpiresAt = expiresAt;
        UserId = userId;
    }

    public bool IsExpired(DateTimeOffset now) => now>= ExpiresAt;

    public void RegisterFailedAttempt(int maxAttempts)
    {
        EnsureIssued();
        FailedAttempts++;
        if (FailedAttempts >= maxAttempts)
            Resolve(OtpStatus.AttemptsExceeded);
    }

    public void MarkVerified() => Resolve(OtpStatus.Verified);
    public void MarkSendFailed() => Resolve(OtpStatus.SendFailed);
    public void Supersede() => Resolve(OtpStatus.Superseded);
    public void Expire() => Resolve(OtpStatus.Expired);

    private void EnsureIssued()
    {
        if (Status != OtpStatus.Issued)
            throw new InvalidOtpStatusTransitionException();
    }

    private void Resolve(OtpStatus status)
    {
        EnsureIssued();
        Status = status;
        ResolvedAt = DateTimeOffset.UtcNow;
    }
}