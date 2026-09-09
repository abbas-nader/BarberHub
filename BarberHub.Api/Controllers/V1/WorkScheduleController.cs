using Asp.Versioning;
using BarberHub.Api.Constants.WorkSchedule;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.WorkSchedule;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class WorkScheduleController(WorkScheduleService workScheduleService) : BaseController
{
    [HttpGet(WorkScheduleUriConstants.GetAllByBarberId)]
    public async Task<ApiResult<IReadOnlyList<WorkScheduleResponse>>> GetAllByBarberId([FromRoute] long barberId,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleService.GetAllByBarberIdAsync(barberId, cancellationToken);
        return workSchedule.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(WorkScheduleUriConstants.GetById)]
    public async Task<ApiResult<WorkScheduleResponse>> GetByIdAsync([FromRoute] long workScheduleId,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleService.GetByIdAsync(workScheduleId, cancellationToken);
        return workSchedule.ToResponse();
    }

    [HttpPost(WorkScheduleUriConstants.Create)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<WorkScheduleResponse>> Create([FromBody] CreateWorkScheduleRequest request,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleService.CreateAsync(request.ToDto(), cancellationToken);
        return workSchedule.ToResponse();
    }

    [HttpPut(WorkScheduleUriConstants.Update)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<WorkScheduleResponse>> Update([FromRoute] long workScheduleId,
        [FromBody] UpdateWorkScheduleRequest request,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleService.UpdateAsync(workScheduleId, request.ToDto(), cancellationToken);
        return workSchedule.ToResponse();
    }

    [HttpPatch(WorkScheduleUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<WorkScheduleResponse>> Delete([FromRoute] long workScheduleId,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleService.DeleteAsync(workScheduleId, cancellationToken);
        return workSchedule.ToResponse();
    }
}