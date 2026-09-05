using Asp.Versioning;
using BarberHub.Api.Constants.Salon;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Salon;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class SalonController(SalonService salonService) : BaseController
{
    [HttpGet(SalonUriConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<SalonResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
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
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonResponse>> CreateAsync([FromBody] CreateSalonRequest request,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonService.CreateAsync(request.ToDto(), cancellationToken);
        return salon.ToResponse();
    }

    [HttpPut(SalonUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<SalonResponse>> UpdateAsync([FromBody] UpdateSalonRequest request,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonService.UpdateAsync(request.ToDto(), cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonResponse>> DeleteAsync([FromRoute] long salonId,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.DeleteAsync(salonId, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Activate)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonResponse>> ActivateAsync([FromRoute] long salonId,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.ActivateAsync(salonId, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Deactivate)]
    [Authorize(Roles = nameof(UserRole.PlatformAdmin))]
    public async Task<ApiResult<SalonResponse>> DeactivateAsync([FromRoute] long salonId,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.DeactivateAsync(salonId, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.UpdateDepositAmount)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<SalonResponse>> UpdateDepositAmountAsync(
        UpdateSalonDepositAmountRequest updateSalonDepositAmountRequest, CancellationToken cancellationToken)
    {
        var salon = await salonService.UpdateDepositAmount(updateSalonDepositAmountRequest.ToDto(), cancellationToken);
        return salon.ToResponse();
    }
}