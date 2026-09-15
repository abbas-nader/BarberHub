using BarberHub.Application.DTOs.BarberService;

namespace BarberHub.Application.Services.InterFaces;

public interface IBarberServiceCatalogService
{
    Task<IReadOnlyList<BarberServiceDto>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default);

    Task<BarberServiceDto> GetByIdAsync(long barberServiceId,
        CancellationToken cancellationToken = default);

    Task<BarberServiceDto> CreateAsync(CreateBarberServiceDto createBarberServiceDto,
        CancellationToken cancellationToken = default);

    Task<BarberServiceDto> UpdateAsync(long barberServiceId, UpdateBarberServiceDto updateBarberServiceDto,
        CancellationToken cancellationToken = default);

    Task<BarberServiceDto> DeleteAsync(long barberServiceId, CancellationToken cancellationToken = default);
}