using BarberHub.Application.DTOs;
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

    public async Task<ExceptionLogDto> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var log = await exceptionLogRepository.GetByIdAsync(id, cancellationToken)
                  ?? throw new EntityNotFoundException(nameof(ExceptionLog), id);

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