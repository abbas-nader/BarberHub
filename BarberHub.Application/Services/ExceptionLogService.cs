using BarberHub.Application.DTOs.ExceptionLog;
using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class ExceptionLogService(IExceptionLogRepository exceptionLogRepository)
{
    public async Task<IReadOnlyList<ExceptionLogDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var logs = await exceptionLogRepository.GetAllAsync(cancellationToken);
        return logs.Select(ToDto).ToList();
    }

    public async Task<ExceptionLogDto> GetByIdAsync(string exceptionId, CancellationToken cancellationToken = default)
    {
        var log = await exceptionLogRepository.GetByIdAsync(exceptionId, cancellationToken)
                  ?? throw new EntityNotFoundException(nameof(ExceptionLog), exceptionId);

        return ToDto(log);
    }

    public async Task<IReadOnlyList<ExceptionLogDto>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
    {
        var logs = await exceptionLogRepository.GetRecentAsync(count, cancellationToken);
        return logs.Select(ToDto).ToList();
    }

    private static ExceptionLogDto ToDto(ExceptionLog log) => new(
        log.Id!,
        log.ExceptionType,
        log.Message,
        log.StackTrace,
        log.Source,
        log.RequestPath,
        log.RequestMethod,
        log.StatusCode,
        log.CreatedAt);
}