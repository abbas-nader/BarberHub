using BarberHub.Application.DTOs.Salon;
using BarberHub.Application.DTOs.Shared;

namespace BarberHub.Application.Services.InterFaces;

public interface ISalonService
{
    Task<IReadOnlyList<SalonDto>> GetAll(CancellationToken cancellationToken = default);
    Task<SalonDto> GetById(long salonId, CancellationToken cancellationToken = default);

    Task<PagedResult<SalonDto>> PaginatedGetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default);

    Task<SalonDto> CreateAsync(CreateSalonDto createSalonDto, CancellationToken cancellationToken = default);
    Task<SalonDto> UpdateAsync(UpdateSalonDto updateSalonDto, CancellationToken cancellationToken = default);
    Task<SalonDto> DeleteAsync(long salonId, CancellationToken cancellationToken = default);
    Task<SalonDto> ActivateAsync(long salonId, CancellationToken cancellationToken = default);
    Task<SalonDto> DeactivateAsync(long salonId, CancellationToken cancellationToken = default);

    Task<SalonDto> UpdateDepositAmount(UpdateSalonDepositAmountDto updateSalonDepositAmountDto,
        CancellationToken cancellationToken);
}