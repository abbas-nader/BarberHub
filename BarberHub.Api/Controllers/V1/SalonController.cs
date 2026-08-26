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
    public async Task<ApiResult<SalonResponse>> GetByIdAsync(long salonId,
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
    public async Task<ApiResult<SalonResponse>> UpdateAsync([FromBody] UpdateSalonRequest request, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonService.UpdateAsync(request.ToDto(), modifiedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Delete)]
    public async Task<ApiResult<SalonResponse>> DeleteAsync(long id, long deletedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.DeleteAsync(id, deletedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Activate)]
    public async Task<ApiResult<SalonResponse>> ActivateAsync(long id, long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.ActivateAsync(id, modifiedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.Deactivate)]
    public async Task<ApiResult<SalonResponse>> DeactivateAsync(long id, long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonService.DeactivateAsync(id, modifiedBy, cancellationToken);
        return salon.ToResponse();
    }

    [HttpPatch(SalonUriConstants.UpdateDepositAmount)]
    public async Task<ApiResult<SalonResponse>> UpdateDepositAmountAsync(long id, decimal depositAmountValue,
        Currency depositAmountCurrency,
        long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon =await salonService.UpdateDepositAmount(id, depositAmountValue, depositAmountCurrency, modifiedBy,
            cancellationToken);
        return salon.ToResponse();
    }
}