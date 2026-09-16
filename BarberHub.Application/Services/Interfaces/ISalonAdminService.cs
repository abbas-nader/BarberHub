using BarberHub.Application.DTOs.SalonAdmin;
using BarberHub.Application.DTOs.Shared;

namespace BarberHub.Application.Services.InterFaces;

public interface ISalonAdminService
{
    Task<IReadOnlyList<SalonAdminDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default);

    Task<SalonAdminDto> GetByIdAsync(long salonAdminId, CancellationToken cancellationToken = default);

    Task<SalonAdminDto> CreateAsync(CreateSalonAdminDto createSalonAdminDto,
        CancellationToken cancellationToken = default);

    Task<SalonAdminDto> UpdateAsync(long salonAdminId, UpdateSalonAdminDto updateSalonAdminDto,
        CancellationToken cancellationToken = default);

    Task<SalonAdminDto> DeleteAsync(long salonAdminId, CancellationToken cancellationToken = default);
}