namespace BarberHub.Application.Security.Jwt;

public interface ICurrentUserService
{
    TokenClaims CurrentUser { get; }
}