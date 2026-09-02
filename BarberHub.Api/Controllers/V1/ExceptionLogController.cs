using Asp.Versioning;
using BarberHub.Api.Constants.ExceptionLog;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.ExceptionLog;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
[Authorize(Roles = nameof(UserRole.PlatformAdmin))]
public class ExceptionLogController(ExceptionLogService exceptionLogService) : BaseController
{
    [HttpGet(ExceptionLogUriConstants.GetAll)]
    public async Task<ApiResult<IReadOnlyList<ExceptionLogResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var logs = await exceptionLogService.GetAllAsync(cancellationToken);
        return logs.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(ExceptionLogUriConstants.GetRecent)]
    public async Task<ApiResult<IReadOnlyList<ExceptionLogResponse>>> GetRecentAsync(int count,
        CancellationToken cancellationToken)
    {
        var logs = await exceptionLogService.GetRecentAsync(count, cancellationToken);
        return logs.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(ExceptionLogUriConstants.GetById)]
    public async Task<ApiResult<ExceptionLogResponse>> GetByIdAsync(string exceptionId,
        CancellationToken cancellationToken)
    {
        var log = await exceptionLogService.GetByIdAsync(exceptionId, cancellationToken);
        return log.ToResponse();
    }
}