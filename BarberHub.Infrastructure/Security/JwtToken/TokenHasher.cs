using System.Security.Cryptography;
using System.Text;
using BarberHub.Application.Security.JwtToken;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Infrastructure.Security.JwtToken;

public class TokenHasher: ITokenHasher
{
    public string Hash(string token)
    {
        if(string.IsNullOrWhiteSpace(token))
            throw new RequiredFieldException(nameof(token));
        
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToHexString(hashBytes);
    }
}