using Asp.Versioning;
using BarberHub.Api.Constants.Auth;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Auth;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
[AllowAnonymous]
public class AuthController(AuthenticationService authenticationService) : BaseController
{
    [HttpPost(AuthUriConstants.Login)]
    public async Task<ApiResult<TokenResponse>> Login([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.LoginAsync(request.ToDto(), cancellationToken);
        return result.ToResponse();
    }
    [HttpPost(AuthUriConstants.Refresh)]
    public async Task<ApiResult<TokenResponse>> Refresh([FromBody] RefreshRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
        return result.ToResponse();
    }

    [HttpPost(AuthUriConstants.Revoke)]
    public async Task<ApiResult> Revoke([FromBody] RefreshRequest request,
        CancellationToken cancellationToken = default)
    {
        await authenticationService.RevokeAsync(request.RefreshToken, cancellationToken);
        return ApiResult.Succeeded();
    }
}