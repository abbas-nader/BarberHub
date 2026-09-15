using BarberHub.Application.DTOs.WorkSchedule;

namespace BarberHub.Application.Services.InterFaces;

public interface IWorkScheduleService
{
    Task<IReadOnlyList<WorkScheduleDto>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default);

    Task<WorkScheduleDto> GetByIdAsync(long workScheduleId, CancellationToken cancellationToken = default);

    Task<WorkScheduleDto> CreateAsync(CreateWorkScheduleDto createWorkScheduleDto,
        CancellationToken cancellationToken = default);

    Task<WorkScheduleDto> UpdateAsync(long workScheduleId, UpdateWorkScheduleDto updateWorkScheduleDto,
        CancellationToken cancellationToken = default);

    Task<WorkScheduleDto> DeleteAsync(long workScheduleId, CancellationToken cancellationToken = default);
}