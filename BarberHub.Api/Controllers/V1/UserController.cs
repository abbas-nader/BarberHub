using Asp.Versioning;
using BarberHub.Api.Constants.User;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Shared;
using BarberHub.Api.Contracts.User;
using BarberHub.Api.Mappers;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class UserController(IUserService userService, ICurrentUserService currentUserService) : BaseController
{
    [HttpGet(UserUriConstants.GetMe)]
    public async Task<ApiResult<UserResponse>> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(currentUserService.CurrentUser.UserId, cancellationToken);
        return user.ToResponse();
    }

    [HttpPatch(UserUriConstants.ChangePassword)]
    public async Task<ApiResult<UserResponse>> ChangePasswordAsync([FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.CurrentUser.UserId;
        var user = await userService.ChangePasswordAsync(userId, request.ToDto(), cancellationToken);
        return user.ToResponse();
    }
}