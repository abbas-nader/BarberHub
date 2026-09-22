namespace BarberHub.Application.Security.Authentication;

public interface ITokenHasher
{
    string Hash(string token);
}