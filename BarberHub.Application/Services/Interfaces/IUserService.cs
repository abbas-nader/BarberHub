using BarberHub.Application.DTOs.Shared;
using BarberHub.Application.DTOs.User;

namespace BarberHub.Application.Services.InterFaces;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(long userId, CancellationToken cancellationToken = default);

    Task<UserDto> CreateAsync(CreateUserDto createUserDto, long creationBy,
        CancellationToken cancellationToken = default);

    Task<UserDto> UpdateAsync(long userId, UpdateUserDto updateUserDto, long modifiedBy,
        CancellationToken cancellationToken = default);

    Task<UserDto> ChangePasswordAsync(long userId, ChangePasswordDto changePasswordDto,
        CancellationToken cancellationToken = default);

    Task<UserDto> DeleteAsync(long userId, long deletedBy,
        CancellationToken cancellationToken = default);
}