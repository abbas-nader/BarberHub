using BarberHub.Application.DTOs.Service;

namespace BarberHub.Application.Services.InterFaces;

public interface IServiceCatalogService
{
    Task<IReadOnlyList<ServiceDto>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);
    Task<ServiceDto> GetByIdAsync(long serviceId, CancellationToken cancellationToken = default);
    Task<ServiceDto> CreateAsync(CreateServiceDto createServiceDto, CancellationToken cancellationToken = default);

    Task<ServiceDto> UpdateAsync(long serviceId, UpdateServiceDto updateServiceDto,
        CancellationToken cancellationToken = default);

    Task<ServiceDto> DeleteAsync(long serviceId, CancellationToken cancellationToken = default);
}