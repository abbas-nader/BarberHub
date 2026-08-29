namespace BarberHub.Application.Security.JwtToken;

public interface ICurrentUserService
{
    TokenClaims CurrentUser { get; }
}