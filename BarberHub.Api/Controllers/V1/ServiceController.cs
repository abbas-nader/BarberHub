using Asp.Versioning;
using BarberHub.Api.Constants.Service;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Service;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class ServiceController(ServiceCatalogService catalogService) : BaseController
{
    [HttpGet(ServiceUriConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<ServiceResponse>>> GetAll([FromRoute] long salonId,
        CancellationToken cancellationToken = default)
    {
        var services = await catalogService.GetAllBySalonIdAsync(salonId, cancellationToken);
        return services.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(ServiceUriConstants.GetById)]
    public async Task<ApiResult<ServiceResponse>> GetByIdAsync([FromRoute] long serviceId,
        CancellationToken cancellationToken = default)
    {
        var service = await catalogService.GetByIdAsync(serviceId, cancellationToken);
        return service.ToResponse();
    }

    [HttpPost(ServiceUriConstants.Create)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<ServiceResponse>> CreateAsync([FromBody] CreateServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var services = await catalogService.CreateAsync(request.ToDto(), cancellationToken);
        return services.ToResponse();
    }

    [HttpPut(ServiceUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<ServiceResponse>> UpdateAsync(long serviceId, [FromBody] UpdateServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var service = await catalogService.UpdateAsync(serviceId, request.ToDto(), cancellationToken);
        return service.ToResponse();
    }

    [HttpPatch(ServiceUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<ServiceResponse>> DeleteAsync(long serviceId,
        CancellationToken cancellationToken = default)
    {
        var service = await catalogService.DeleteAsync(serviceId, cancellationToken);
        return service.ToResponse();
    }
}