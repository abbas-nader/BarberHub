using BarberHub.Application.DTOs.Otp;

namespace BarberHub.Application.Services.InterFaces;

public interface IOtpService
{
    Task SendMobileVerificationCodeAsync(CancellationToken cancellationToken = default);
    Task VerifyMobileAsync(VerifyOtpDto dto, CancellationToken cancellationToken = default);
}