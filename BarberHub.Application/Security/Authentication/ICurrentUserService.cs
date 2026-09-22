namespace BarberHub.Application.Security.Authentication;

public interface ICurrentUserService
{
    TokenClaims CurrentUser { get; }
}