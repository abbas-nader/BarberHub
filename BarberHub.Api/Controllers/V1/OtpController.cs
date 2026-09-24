using Asp.Versioning;
using BarberHub.Api.Constants.Otp;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Otp;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.EndUser))]
public class OtpController(IOtpService otpService) : BaseController
{
    [HttpPost(OtpUriConstants.Send)]
    [EnableRateLimiting("otp-send")]
    public async Task<ApiResult> SendAsync(CancellationToken cancellationToken = default)
    {
        await otpService.SendMobileVerificationCodeAsync(cancellationToken);
        return ApiResult.Succeeded();
    }

    [HttpPost(OtpUriConstants.Verify)]
    [EnableRateLimiting("otp-verify")]
    public async Task<ApiResult> VerifyAsync([FromBody] VerifyOtpRequest request,
        CancellationToken cancellationToken = default)
    {
        await otpService.VerifyMobileAsync(request.ToDto(), cancellationToken);
        return ApiResult.Succeeded();
    }
}