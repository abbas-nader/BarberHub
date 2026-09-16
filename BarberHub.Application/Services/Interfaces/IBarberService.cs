using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.DTOs.Shared;

namespace BarberHub.Application.Services.InterFaces;

public interface IBarberService
{
    Task<IReadOnlyList<BarberDto>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);
    Task<BarberDto> GetByIdAsync(long barberId, CancellationToken cancellationToken = default);
    Task<BarberDto> CreateAsync(CreateBarberDto createBarberDto, CancellationToken cancellationToken = default);

    Task<BarberDto> UpdateAsync(long barberId, UpdateBarberDto updateBarberDto,
        CancellationToken cancellationToken = default);

    Task<BarberDto> DeleteAsync(long barberId, CancellationToken cancellationToken = default);
    Task<BarberDto> ActivateAsync(long barberId, CancellationToken cancellationToken = default);
    Task<BarberDto> DeactivateAsync(long barberId, CancellationToken cancellationToken = default);
}