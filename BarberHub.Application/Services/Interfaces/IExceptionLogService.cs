using BarberHub.Application.DTOs.ExceptionLog;

namespace BarberHub.Application.Services.InterFaces;

public interface IExceptionLogService
{
    Task<IReadOnlyList<ExceptionLogDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExceptionLogDto> GetByIdAsync(string exceptionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExceptionLogDto>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
}