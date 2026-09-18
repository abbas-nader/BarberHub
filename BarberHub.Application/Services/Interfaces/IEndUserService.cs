using BarberHub.Application.DTOs.EndUser;

namespace BarberHub.Application.Services.InterFaces;

public interface IEndUserService
{
    Task<IReadOnlyList<EndUserDto>> GetAll(CancellationToken cancellationToken = default);
    Task<EndUserDto> GetByIdAsync(long endUserId, CancellationToken cancellationToken = default);

    Task<EndUserDto> UpdateAsync(UpdateEndUserDto updateEndUserDto, CancellationToken cancellationToken = default);

    Task<EndUserDto> DeleteAsync(CancellationToken cancellationToken = default);
}