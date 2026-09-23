using BarberHub.Application.Sms;
using BarberHub.Domain.Exceptions;
using Kavenegar;
using Kavenegar.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure.Sms.Kavenegar;

public class KavenegarSmsSender(
    IKavenegarApi kavenegarApi,
    IOptions<KavenegarSetting> options,
    ILogger<KavenegarSmsSender> logger) : ISmsSender

{
    public async Task SendOtpAsync(string mobileNumber, string code, CancellationToken cancellationToken = default)
    {
        try
        {
            await kavenegarApi.VerifyLookupAsync(receptor: mobileNumber, token: code,
                template: options.Value.OtpTemplate);
        }
        catch (ApiException  ex)
        {
            logger.LogWarning("Kavenegar rejected the OTP request. Code: {Code}", ex.Code);
            throw new SmsSendFailedException();
        }
        catch (HttpException ex)
        {
            logger.LogError(ex, "Kavenegar OTP request failed (network).");
            throw new SmsSendFailedException();
        }
    }
}