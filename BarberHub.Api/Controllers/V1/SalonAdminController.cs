using Asp.Versioning;
using BarberHub.Api.Constants.SalonAdmin;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.SalonAdmin;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class SalonAdminController(SalonAdminService salonAdminService) : BaseController
{
    [HttpGet(SalonAdminUriConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<SalonAdminResponse>>> GetAllAsync(long salonAdminId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmins = await salonAdminService.GetAllBySalonIdAsync(salonAdminId, cancellationToken);
        return salonAdmins.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(SalonAdminUriConstants.GetById)]
    public async Task<ApiResult<SalonAdminResponse>> GetByIdAsync(long salonAdminId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminService.GetByIdAsync(salonAdminId, cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPost(SalonAdminUriConstants.Create)]
    public async Task<ApiResult<SalonAdminResponse>> CreateAsync(
        [FromBody] CreateSalonAdminRequest createSalonAdminRequest,
        long creationBy,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin =
            await salonAdminService.CreateAsync(createSalonAdminRequest.ToDto(), creationBy, cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPut(SalonAdminUriConstants.Update)]
    public async Task<ApiResult<SalonAdminResponse>> UpdateAsync(
        long salonAdminId,
        [FromBody] UpdateSalonAdminRequest updateSalonAdminRequest,
        long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin =
            await salonAdminService.UpdateAsync(salonAdminId, updateSalonAdminRequest.ToDto(), modifiedBy,
                cancellationToken);
        return salonAdmin.ToResponse();
    }

    [HttpPatch(SalonAdminUriConstants.Delete)]
    public async Task<ApiResult<SalonAdminResponse>> DeleteAsync(long salonAdminId, long deletedBy,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminService.DeleteAsync(salonAdminId, deletedBy, cancellationToken);
        return salonAdmin.ToResponse();
    }
}