using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BarberHub.Application.Security.JwtToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BarberHub.Infrastructure.Security.JwtToken;

public class JwtTokenGenerator(IOptions<JwtSetting> options) : IJwtTokenGenerator
{
    private readonly JwtSetting _jwtSettings = options.Value;

    public TokenResult Generate(TokenClaims tokenClaims)
    {
        var accessTokenExpirest = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
        var accessToken = GenerateAccessToken(tokenClaims, accessTokenExpirest);

        var refreshTokenExpirest = DateTimeOffset.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        var refreshToken = GenerateRefreshToken();

        return new TokenResult(accessToken, accessTokenExpirest, refreshToken, refreshTokenExpirest);
    }

    private string GenerateAccessToken(TokenClaims tokenClaims, DateTimeOffset expiresAt)
    {
        var claimsList = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, tokenClaims.UserId.ToString()),
            new(ClaimTypes.Role, tokenClaims.UserRole.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        if (tokenClaims.SalonId is not null)
            claimsList.Add(new Claim(JwtCustomClaimNames.SalonId, tokenClaims.SalonId.Value.ToString()));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claimsList,
            expiresAt.UtcDateTime,
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(randomBytes);
    }
}