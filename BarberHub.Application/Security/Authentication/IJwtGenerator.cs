namespace BarberHub.Application.Security.Authentication;

public interface IJwtGenerator
{
    TokenResult Generate(TokenClaims  tokenClaims);
}