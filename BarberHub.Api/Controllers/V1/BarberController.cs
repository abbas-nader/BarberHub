using Asp.Versioning;
using BarberHub.Api.Constants.Barber;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class BarberController(BarberService barberService) : BaseController
{
    [HttpGet(BarberUriConstants.GetAllBySalonId)]
    public async Task<ApiResult<IReadOnlyList<BarberResponse>>> GetAllBySalonIdAsync([FromRoute] long salonId,
        CancellationToken cancellationToken)
    {
        var barbers = await barberService.GetAllBySalonIdAsync(salonId, cancellationToken);
        return barbers.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(BarberUriConstants.GetById)]
    public async Task<ApiResult<BarberResponse>> GetByIdAsync([FromRoute] long barberId,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.GetByIdAsync(barberId, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPost(BarberUriConstants.Create)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberResponse>> CreateAsync([FromBody] CreateBarberRequest request,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.CreateAsync(request.ToDto(), cancellationToken);
        return barber.ToResponse();
    }

    [HttpPut(BarberUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberResponse>> UpdateAsync([FromRoute] long barberId,
        [FromBody] UpdateBarberRequest request,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.UpdateAsync(barberId, request.ToDto(), cancellationToken);
        return barber.ToResponse();
    }

    [HttpPatch(BarberUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberResponse>> DeleteAsync([FromRoute] long barberId,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.DeleteAsync(barberId, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPatch(BarberUriConstants.Activate)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberResponse>> ActivateAsync([FromRoute] long barberId,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.ActivateAsync(barberId, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPatch(BarberUriConstants.Deactivate)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberResponse>> DeactivateAsync([FromRoute] long barberId,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.DeactivateAsync(barberId, cancellationToken);
        return barber.ToResponse();
    }
}