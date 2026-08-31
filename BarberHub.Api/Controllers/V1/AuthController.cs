using Asp.Versioning;
using BarberHub.Api.Constants.Authentication;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Auth;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class AuthController(AuthenticationService authenticationService) : BaseController
{
    [HttpPost(AuthUriConstants.LoginSalonAdmin)]
    public async Task<ApiResult<TokenResponse>> LoginSalonAdmin([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.LoginSalonAdminAsync(request.ToDto(), cancellationToken);
        return result.ToResponse();
    }

    [HttpPost(AuthUriConstants.LoginBarber)]
    public async Task<ApiResult<TokenResponse>> LoginBarber([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.LoginBarberAsync(request.ToDto(), cancellationToken);
        return result.ToResponse();
    }

    [HttpPost(AuthUriConstants.LoginCustomer)]
    public async Task<ApiResult<TokenResponse>> LoginCustomer([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.LoginCustomerAsync(request.ToDto(), cancellationToken);
        return result.ToResponse();
    }

    [HttpPost(AuthUriConstants.LoginPlatformAdmin)]
    public async Task<ApiResult<TokenResponse>> LoginPlatformAdmin([FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.LoginPlatformAdminAsync(request.ToDto(), cancellationToken);
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