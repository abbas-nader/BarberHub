using BarberHub.Application.DTOs.PlatformAdmin;

namespace BarberHub.Application.Services.InterFaces;

public interface IPlatformAdminService
{
    Task<IReadOnlyList<PlatformAdminDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlatformAdminDto> GetByIdAsync(long platformAdminId, CancellationToken cancellationToken = default);

    Task<PlatformAdminDto> CreateAsync(CreatePlatformAdminDto createPlatformAdminDto,
        CancellationToken cancellationToken = default);

    Task<PlatformAdminDto> UpdateAsync(UpdatePlatformAdminDto createPlatformAdminDto,
        CancellationToken cancellationToken = default);

    Task<PlatformAdminDto> DeleteAsync(long platformAdminId,CancellationToken cancellationToken = default);
}