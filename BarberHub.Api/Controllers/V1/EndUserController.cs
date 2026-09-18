using Asp.Versioning;
using BarberHub.Api.Constants.EndUser;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.EndUser;
using BarberHub.Api.Mappers;
using BarberHub.Application.DTOs.EndUser;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class EndUserController(IEndUserService endUserService) : BaseController
{
    [HttpGet(EndUserUriConstants.GetAll)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<IReadOnlyList<EndUserResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var endUsers = await endUserService.GetAll(cancellationToken);
        return endUsers.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(EndUserUriConstants.GetById)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<EndUserResponse>> GetByIdAsync([FromRoute] long endUserId,
        CancellationToken cancellationToken = default)
    {
        var endUser = await endUserService.GetByIdAsync(endUserId, cancellationToken);
        return endUser.ToResponse();
    }

    [HttpPut(EndUserUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.EndUser))]
    public async Task<ApiResult<EndUserResponse>> UpdateAsync([FromRoute] long endUserId,
        [FromBody] UpdateEndUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var endUser = await endUserService.UpdateAsync(endUserId, request.ToDto(), cancellationToken);
        return endUser.ToResponse();
    }

    [HttpPut(EndUserUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.EndUser))]
    public async Task<ApiResult<EndUserResponse>> DeleteAsync([FromRoute] long endUserId,
        CancellationToken cancellationToken = default)
    {
        var endUser = await endUserService.DeleteAsync(endUserId, cancellationToken);
        return endUser.ToResponse();
    }
}