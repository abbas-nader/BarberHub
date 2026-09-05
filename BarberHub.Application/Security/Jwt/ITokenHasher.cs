namespace BarberHub.Application.Security.Jwt;

public interface ITokenHasher
{
    string Hash(string token);
}