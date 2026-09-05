namespace BarberHub.Application.Security.Jwt;

public interface IJwtGenerator
{
    TokenResult Generate(TokenClaims  tokenClaims);
}