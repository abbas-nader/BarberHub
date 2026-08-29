namespace BarberHub.Application.Security.JwtToken;

public interface IJwtTokenGenerator
{
    TokenResult Generate(TokenClaims  tokenClaims);
}