namespace BarberHub.Application.Security.JwtToken;

public interface ITokenHasher
{
    string Hash(string token);
}