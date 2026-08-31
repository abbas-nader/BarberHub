using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BarberHub.Application.Security.JwtToken;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Api.Security;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private TokenClaims? _currentUser;
    public TokenClaims CurrentUser => _currentUser ??= BuildTokenClaims();

    private TokenClaims BuildTokenClaims()
    {
        var user = httpContextAccessor.HttpContext?.User
                   ?? throw new CurrentUserContextUnavailableException();
        if (user.Identity is { IsAuthenticated: false })
            throw new UserNotAuthenticatedException();
        var userId = long.Parse(GetRequiredClaim(user, JwtRegisteredClaimNames.Sub));
        var userRole = Enum.Parse<UserRole>(GetRequiredClaim(user, ClaimTypes.Role));

        var salonIdClaim = user.FindFirst(JwtCustomClaimNames.SalonId)?.Value;
        long? salonId = string.IsNullOrWhiteSpace(salonIdClaim)
            ? null
            : long.Parse(salonIdClaim);
        return new TokenClaims(userId, userRole, salonId);
    }

    private static string GetRequiredClaim(ClaimsPrincipal user, string claimType)
        => user.FindFirst(claimType)?.Value
           ?? throw new RequiredClaimMissingException(nameof(claimType));
}