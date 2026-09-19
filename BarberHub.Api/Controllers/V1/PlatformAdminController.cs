using Asp.Versioning;
using BarberHub.Api.Constants.PlatformAdmin;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.PlatformAdmin;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.PlatformAdmin))]
public class PlatformAdminController(IPlatformAdminService platformAdminService) : BaseController
{
    [HttpGet(PlatformAdminUriConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<PlatformAdminResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var platformAdmins = await platformAdminService.GetAllAsync(cancellationToken);
        return platformAdmins.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(PlatformAdminUriConstants.GetById)]
    public async Task<ApiResult<PlatformAdminResponse>> GetByIdAsync([FromRoute] long platformAdminId,
        CancellationToken cancellationToken = default)
    {
        var platformAdmin = await platformAdminService.GetByIdAsync(platformAdminId, cancellationToken);
        return platformAdmin.ToResponse();
    }

    [HttpPost(PlatformAdminUriConstants.Create)]
    public async Task<ApiResult<PlatformAdminResponse>> CreateAsync(
        [FromBody] CreatePlatformAdminRequest createPlatformAdminRequest, CancellationToken cancellationToken = default)
    {
        var platformAdmin =
            await platformAdminService.CreateAsync(createPlatformAdminRequest.ToDto(), cancellationToken);
        return platformAdmin.ToResponse();
    }

    [HttpPut(PlatformAdminUriConstants.Update)]
    public async Task<ApiResult<PlatformAdminResponse>> UpdateAsync(
        [FromBody] UpdatePlatformAdminRequest updatePlatformAdminRequest, CancellationToken cancellationToken = default)
    {
        var platformAdmin =
            await platformAdminService.UpdateAsync(updatePlatformAdminRequest.ToDto(), cancellationToken);
        return platformAdmin.ToResponse();
    }

    [HttpPatch(PlatformAdminUriConstants.Delete)]
    public async Task<ApiResult<PlatformAdminResponse>> DeleteAsync([FromRoute] long platformAdminId,
        CancellationToken cancellationToken = default)
    {
        var platformAdmin = await platformAdminService.DeleteAsync(platformAdminId, cancellationToken);
        return platformAdmin.ToResponse();
    }
}