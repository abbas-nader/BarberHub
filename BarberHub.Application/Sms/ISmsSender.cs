namespace BarberHub.Application.Sms;

public interface ISmsSender
{
    Task SendOtpAsync(string mobileNumber, string code, CancellationToken cancellationToken = default);
}