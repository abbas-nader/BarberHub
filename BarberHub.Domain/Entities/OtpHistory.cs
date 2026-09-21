using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class OtpHistory
{
    public long Id { get; private set; }
    public OtpPurpose Purpose { get; private set; }
    public long UserId { get; private set; }
    public OtpStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? ResolvedAt { get; private set; }

    private OtpHistory()
    {
    }

    public OtpHistory(OtpPurpose purpose, long userId, DateTimeOffset expiresAt)
    {
        Purpose = purpose;
        UserId = userId;
        Status = OtpStatus.Issued;
        CreatedAt = DateTimeOffset.UtcNow;
        ExpiresAt = expiresAt;
    }

    public void MarkVerified() => Resolve(OtpStatus.Verified);

    public void MarkAttemptsExceeded() => Resolve(OtpStatus.AttemptsExceeded);

    public void MarkSendFailed() => Resolve(OtpStatus.SendFailed);

    private void Resolve(OtpStatus status)
    {
        if (Status != OtpStatus.Issued)
            throw new InvalidOtpStatusTransitionException();

        Status = status;
        ResolvedAt = DateTimeOffset.UtcNow;
    }
}