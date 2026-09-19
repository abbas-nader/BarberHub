using BarberHub.Application.DTOs.Shared;
using BarberHub.Application.DTOs.User;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services.Implements;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IEndUserRepository endUserRepository,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<UserDto> GetByIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), userId);
        return ToDto(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, long creationBy,
        CancellationToken cancellationToken = default)
    {
        if (await userRepository.ExistsByUserNameAsync(dto.UserName, cancellationToken))
            throw new DuplicateUserNameException();

        var passwordHash = passwordHasher.Hash(dto.Password);
        var user = new User(dto.FirstName, dto.LastName, dto.UserName, passwordHash, dto.Role,
            dto.MobileNumber, creationBy);

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
        return ToDto(user);
    }

    public async Task<UserDto> UpdateAsync(long userId, UpdateUserDto dto, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), userId);

        if (!string.Equals(user.UserName, dto.UserName, StringComparison.Ordinal))
        {
            if (await userRepository.ExistsByUserNameAsync(dto.UserName, cancellationToken))
                throw new DuplicateUserNameException();
        }

        user.Update(dto.FirstName, dto.LastName, dto.UserName, dto.MobileNumber, modifiedBy);
        userRepository.Update(user);
        await userRepository.SaveChangesAsync(cancellationToken);
        return ToDto(user);
    }

    public async Task<UserDto> ChangePasswordAsync(long userId, ChangePasswordDto dto,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), userId);

        if (!passwordHasher.Verify(dto.OldPassword, user.PasswordHash))
            throw new InvalidCurrentPasswordException();

        user.ChangePassword(passwordHasher.Hash(dto.NewPassword), userId);
        userRepository.Update(user);
        await userRepository.SaveChangesAsync(cancellationToken);
        return ToDto(user);
    }

    public async Task<UserDto> DeleteAsync(long userId, long deletedBy,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), userId);
        
            user.SoftDelete(deletedBy);
            userRepository.Update(user);
            await userRepository.SaveChangesAsync(cancellationToken);
            return ToDto(user);
    }

    private static UserDto ToDto(User user)
        => new(
            user.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.MobileNumber,
            user.IsMobileVerified
        );
}