namespace BarberHub.Domain.Enums;

public enum OtpStatus
{
    Issued = 1,
    Verified = 2,
    AttemptsExceeded = 3,
    SendFailed = 4,
    Superseded = 5,
    Expired = 6
}