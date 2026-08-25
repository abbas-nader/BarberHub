using Asp.Versioning;
using BarberHub.Api.Constants.Barber;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class BarberController(BarberService barberService) : BaseController
{
    [HttpGet(BarberUriConstants.GetAllBySalonId)]
    public async Task<ApiResult<IReadOnlyList<BarberResponse>>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken)
    {
        var barbers = await barberService.GetAllBySalonIdAsync(salonId, cancellationToken);
        return barbers.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(BarberUriConstants.GetById)]
    public async Task<ApiResult<BarberResponse>> GetByIdAsync(long barberId, CancellationToken cancellationToken)
    {
        var barber = await barberService.GetByIdAsync(barberId, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPost(BarberUriConstants.Create)]
    public async Task<ApiResult<BarberResponse>> CreateAsync([FromBody] CreateBarberRequest request,
        long creationBy, CancellationToken cancellationToken)
    {
        var barber = await barberService.CreateAsync(request.ToDto(), creationBy, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPut(BarberUriConstants.Update)]
    public async Task<ApiResult<BarberResponse>> UpdateAsync(long id, [FromBody] UpdateBarberRequest request,
        long modifiedBy, CancellationToken cancellationToken)
    {
        var barber = await barberService.UpdateAsync(request.ToDto(), modifiedBy, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPatch(BarberUriConstants.Delete)]
    public async Task<ApiResult<BarberResponse>> DeleteAsync(long id, long deletedBy,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.DeleteAsync(id, deletedBy, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPatch(BarberUriConstants.Activate)]
    public async Task<ApiResult<BarberResponse>> ActivateAsync(long id, long modifiedBy,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.ActivateAsync(id, modifiedBy, cancellationToken);
        return barber.ToResponse();
    }

    [HttpPatch(BarberUriConstants.Deactivate)]
    public async Task<ApiResult<BarberResponse>> DeactivateAsync(long id, long modifiedBy,
        CancellationToken cancellationToken)
    {
        var barber = await barberService.DeactivateAsync(id, modifiedBy, cancellationToken);
        return barber.ToResponse();
    }
}