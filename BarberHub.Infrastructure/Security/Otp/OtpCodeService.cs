using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using BarberHub.Application.Security.Otp;
using BarberHub.Domain.Constants;
using Microsoft.Extensions.Options;

namespace BarberHub.Infrastructure.Security.Otp;

public class OtpCodeService(IOptions<OtpSetting> options) : IOtpCodeService
{
    private readonly byte[] _key = string.IsNullOrWhiteSpace(options.Value.SecretKey)
        ? throw new InvalidOperationException($"{OtpSetting.SectionName}:SecretKey must be configured.")
        : Encoding.UTF8.GetBytes(options.Value.SecretKey);
    public string Generate()
        => RandomNumberGenerator
            .GetInt32(OtpConstants.CodeMinValue, OtpConstants.CodeMaxValue)
            .ToString(CultureInfo.InvariantCulture);

    public string Hash(string code)
        => Convert.ToHexString(HMACSHA256.HashData(_key, Encoding.UTF8.GetBytes(code)));

    public bool Verify(string code, string codeHash)
    {
        var expected = HMACSHA256.HashData(_key, Encoding.UTF8.GetBytes(code));
        byte[] actual;
        try
        {
            actual = Convert.FromHexString(codeHash);
        }
        catch (FormatException)
        {
            return false;
        }

        return CryptographicOperations.FixedTimeEquals(expected, actual);
    }
}