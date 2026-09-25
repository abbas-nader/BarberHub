using BarberHub.Application.DTOs.Otp;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Authentication;
using BarberHub.Application.Security.Otp;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Application.Sms;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Application.Services.Implements;

public class OtpService(
    IOtpHistoryRepository otpHistoryRepository,
    IUserRepository userRepository,
    IOtpCodeService otpCodeService,
    ISmsSender smsSender,
    ICurrentUserService currentUserService) : IOtpService
{
    private const OtpPurpose Purpose = OtpPurpose.MobileVerification;

    public async Task SendMobileVerificationCodeAsync(CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.CurrentUser.UserId;
        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
                   ?? throw new EntityNotFoundException(nameof(User), userId);

        if (string.IsNullOrWhiteSpace(user.MobileNumber))
            throw new RequiredFieldException(nameof(user.MobileNumber));
        if (user.IsMobileVerified == true)
            throw new MobileNumberAlreadyVerifiedException();
        var now = DateTimeOffset.UtcNow;
        await EnsureNotRateLimitedAsync(userId, user.MobileNumber, now, cancellationToken);
        var activeCodes = await otpHistoryRepository.GetAllIssuedAsync(Purpose, userId, cancellationToken);
        foreach (var old in activeCodes)
        {
            old.Supersede();
            otpHistoryRepository.Update(old);
        }

        var code = otpCodeService.Generate();
        var otp = new OtpHistory(Purpose, otpCodeService.Hash(code),
            now.AddMinutes(OtpConstants.ExpirationMinutes), userId);
        await otpHistoryRepository.AddAsync(otp, cancellationToken);
        await otpHistoryRepository.SaveChangesAsync(cancellationToken);
        try
        {
            await smsSender.SendOtpAsync(user.MobileNumber, code, cancellationToken);
        }
        catch
        {
            otp.MarkSendFailed();
            otpHistoryRepository.Update(otp);
            await otpHistoryRepository.SaveChangesAsync(CancellationToken.None);
            throw;
        }

        var previous = await otpHistoryRepository.GetAllIssuedAsync(Purpose, userId, CancellationToken.None);
        foreach (var old in previous.Where(x => x.Id != otp.Id))
        {
            old.Supersede();
            otpHistoryRepository.Update(old);
        }

        await otpHistoryRepository.SaveChangesAsync(CancellationToken.None);
    }

    public async Task VerifyMobileAsync(VerifyOtpDto dto, CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.CurrentUser.UserId;

        var otp = await otpHistoryRepository.GetLatestIssuedAsync(Purpose, userId, cancellationToken)
                  ?? throw new OtpExpiredException();

        if (otp.IsExpired(DateTimeOffset.UtcNow))
        {
            otp.Expire();
            otpHistoryRepository.Update(otp);
            await otpHistoryRepository.SaveChangesAsync(cancellationToken);
            throw new OtpExpiredException();
        }

        if (!otpCodeService.Verify(dto.Code, otp.CodeHash))
        {
            otp.RegisterFailedAttempt(OtpConstants.MaxFailedAttempts);
            otpHistoryRepository.Update(otp);
            await otpHistoryRepository.SaveChangesAsync(cancellationToken);
            throw new InvalidOtpCodeException();
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
                   ?? throw new EntityNotFoundException(nameof(User), userId);

        otp.MarkVerified();
        user.VerifyMobileNumber(userId);
        otpHistoryRepository.Update(otp);
        userRepository.Update(user);
        await otpHistoryRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureNotRateLimitedAsync(long userId, string mobileNumber, DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var last = await otpHistoryRepository.GetLatestAsync(Purpose, userId, cancellationToken);
        if (last is not null && last.CreatedAt.AddSeconds(OtpConstants.ResendCooldownSeconds) > now)
            throw new OtpRateLimitExceededException();

        var since = now.AddHours(-24);

        var sentByUser = await otpHistoryRepository.CountSinceAsync(Purpose, userId, since, cancellationToken);
        if (sentByUser >= OtpConstants.MaxSendsPer24Hours)
            throw new OtpRateLimitExceededException();

        var sentToMobile =
            await otpHistoryRepository.CountSinceByMobileAsync(Purpose, mobileNumber, since, cancellationToken);
        if (sentToMobile >= OtpConstants.MaxSendsPer24Hours)
            throw new OtpRateLimitExceededException();
    }
}