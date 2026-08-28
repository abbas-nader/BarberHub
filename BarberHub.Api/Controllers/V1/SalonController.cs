using Asp.Versioning;
using BarberHub.Api.Constants.Salon;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Salon;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class SalonController(SalonService salonService) : BaseController
{
    [HttpGet(SalonUriConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<SalonResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var salons = await salonService.GetAll(cancellationToken);
        return salons.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(SalonUriConstants.GetById)]
    public async Task<ApiResult<SalonResponse>> GetByIdAsync([FromRoute] long salonId,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonService.GetById(salonId, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPost(SalonUriConstants.Create)]
    public async Task<ApiResult<SalonResponse>> CreateAsync([FromBody] CreateSalonRequest request, long creationBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonService.CreateAsync(request.ToDto(), creationBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPut(SalonUriConstants.Update)]
    public async Task<ApiResult<SalonResponse>> UpdateAsync([FromRoute] long salonId,
        [FromBody] UpdateSalonRequest request,
        long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonService.UpdateAsync(salonId, request.ToDto(), modifiedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Delete)]
    public async Task<ApiResult<SalonResponse>> DeleteAsync([FromRoute] long salonId, long deletedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.DeleteAsync(salonId, deletedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Activate)]
    public async Task<ApiResult<SalonResponse>> ActivateAsync([FromRoute] long salonId, long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.ActivateAsync(salonId, modifiedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Deactivate)]
    public async Task<ApiResult<SalonResponse>> DeactivateAsync([FromRoute] long salonId, long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.DeactivateAsync(salonId, modifiedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.UpdateDepositAmount)]
    public async Task<ApiResult<SalonResponse>> UpdateDepositAmountAsync([FromRoute] long salonId,
        UpdateSalonDepositAmountRequest updateSalonDepositAmountRequest,
        long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.UpdateDepositAmount(salonId, updateSalonDepositAmountRequest.ToDto(), modifiedBy,
            cancellationToken);
        return salon.ToResponse();
    }
}