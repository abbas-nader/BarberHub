namespace BarberHub.Application.Security.Otp;

public interface IOtpCodeService
{
    string Generate();
    string Hash(string code);
    bool Verify(string code, string codeHash);
}