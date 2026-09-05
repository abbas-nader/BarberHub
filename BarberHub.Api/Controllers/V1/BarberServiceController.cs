using Asp.Versioning;
using BarberHub.Api.Constants.BarberService;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.BarberService;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class BarberServiceController(BarberServiceCatalogService barberServiceCatalogService) : BaseController
{
    [HttpGet(BarberServiceUriConstants.GetAllByBarberId)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<IReadOnlyList<BarberServiceResponse>>> GetAllByBarberIdAsync([FromRoute] long barberId,
        CancellationToken cancellationToken = default)
    {
        var barberServices = await barberServiceCatalogService.GetAllByBarberIdAsync(barberId, cancellationToken);
        return barberServices
            .Select(x => x.ToResponse())
            .ToList();
    }

    [HttpGet(BarberServiceUriConstants.GetById)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberServiceResponse>> GetByIdAsync([FromRoute] long serviceId,
        CancellationToken cancellationToken = default)
    {
        var barberService = await barberServiceCatalogService.GetByIdAsync(serviceId, cancellationToken);
        return barberService.ToResponse();
    }

    [HttpPost(BarberServiceUriConstants.Create)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberServiceResponse>> CreateAsync([FromBody] CreateBarberServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var barberService = await barberServiceCatalogService.CreateAsync(request.ToDto(), cancellationToken);
        return barberService.ToResponse();
    }

    [HttpPut(BarberServiceUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberServiceResponse>> UpdateAsync([FromRoute] long serviceId,
        [FromBody] UpdateBarberServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var barberService =
            await barberServiceCatalogService.UpdateAsync(serviceId, request.ToDto(), cancellationToken);
        return barberService.ToResponse();
    }

    [HttpPatch(BarberServiceUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<BarberServiceResponse>> DeleteAsync([FromRoute] long serviceId,
        CancellationToken cancellationToken = default)
    {
        var barberService =
            await barberServiceCatalogService.DeleteAsync(serviceId, cancellationToken);
        return barberService.ToResponse();
    }
}