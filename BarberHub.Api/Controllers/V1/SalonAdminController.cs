using Asp.Versioning;
using BarberHub.Api.Constants.SalonAdmin;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.SalonAdmin;
using BarberHub.Api.Contracts.Shared;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class SalonAdminController(SalonAdminService salonAdminService) : BaseController
{
    [HttpGet(SalonAdminUriConstants.GetAllBySalonId)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<IReadOnlyList<SalonAdminResponse>>> GetAllAsync([FromRoute]long salonId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmins = await salonAdminService.GetAllBySalonIdAsync(salonId, cancellationToken);
        return salonAdmins.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(SalonAdminUriConstants.GetById)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonAdminResponse>> GetByIdAsync([FromRoute] long salonAdminId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminService.GetByIdAsync(salonAdminId, cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPost(SalonAdminUriConstants.Create)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonAdminResponse>> CreateAsync(
        [FromBody] CreateSalonAdminRequest createSalonAdminRequest,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin =
            await salonAdminService.CreateAsync(createSalonAdminRequest.ToDto(), cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPut(SalonAdminUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<SalonAdminResponse>> UpdateAsync(
        [FromRoute] long salonAdminId,
        [FromBody] UpdateSalonAdminRequest updateSalonAdminRequest,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin =
            await salonAdminService.UpdateAsync(salonAdminId, updateSalonAdminRequest.ToDto(), cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPatch(SalonAdminUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonAdminResponse>> DeleteAsync([FromRoute] long salonAdminId, CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminService.DeleteAsync(salonAdminId, cancellationToken);
        return salonAdmin.ToResponse();
    }
    [HttpPatch(SalonAdminUriConstants.ChangePassword)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<SalonAdminResponse>> ChangePasswordAsync([FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var salonAdmin = await salonAdminService.ChangePasswordAsync(request.ToDto(), cancellationToken);
        return salonAdmin.ToResponse();
    }
}