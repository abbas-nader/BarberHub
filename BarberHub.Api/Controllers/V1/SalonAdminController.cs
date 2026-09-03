using Asp.Versioning;
using BarberHub.Api.Constants.SalonAdmin;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.SalonAdmin;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.PlatformAdmin))]
public class SalonAdminController(SalonAdminService salonAdminService) : BaseController
{
    [HttpGet(SalonAdminUriConstants.GetAllBySalonId)]
    public async Task<ApiResult<IReadOnlyList<SalonAdminResponse>>> GetAllAsync([FromRoute]long salonId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmins = await salonAdminService.GetAllBySalonIdAsync(salonId, cancellationToken);
        return salonAdmins.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(SalonAdminUriConstants.GetById)]
    public async Task<ApiResult<SalonAdminResponse>> GetByIdAsync([FromRoute] long salonAdminId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminService.GetByIdAsync(salonAdminId, cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPost(SalonAdminUriConstants.Create)]
    public async Task<ApiResult<SalonAdminResponse>> CreateAsync(
        [FromBody] CreateSalonAdminRequest createSalonAdminRequest,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin =
            await salonAdminService.CreateAsync(createSalonAdminRequest.ToDto(), cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPut(SalonAdminUriConstants.Update)]
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
    public async Task<ApiResult<SalonAdminResponse>> DeleteAsync([FromRoute] long salonAdminId, CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminService.DeleteAsync(salonAdminId, cancellationToken);
        return salonAdmin.ToResponse();
    }
}