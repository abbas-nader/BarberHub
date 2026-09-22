using BarberHub.Application.DTOs.PlatformAdmin;
using BarberHub.Application.DTOs.User;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Authentication;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services.Implements;

public class PlatformAdminService(
    IPlatformRepository platformAdminRepository,
    IUserRepository userRepository,
    IUserService userService,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IPlatformAdminService
{
    public async Task<IReadOnlyList<PlatformAdminDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var admins = await platformAdminRepository.GetAllAsync(cancellationToken);
        var users = await userRepository.GetByIdsAsync(admins.Select(x => x.UserId), cancellationToken);
        return admins.Select(x => ToDto(x, users[x.UserId])).ToList();
    }

    public async Task<PlatformAdminDto> GetByIdAsync(long platformAdminId,
        CancellationToken cancellationToken = default)
    {
        var admin = await platformAdminRepository.GetByIdAsync(platformAdminId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(PlatformAdmin), platformAdminId);
        var user = await userRepository.GetByIdAsync(admin.UserId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), admin.UserId);
        return ToDto(admin, user);
    }

    public async Task<PlatformAdminDto> CreateAsync(CreatePlatformAdminDto dto,
        CancellationToken cancellationToken = default)
    {
        var currentUserId = currentUserService.CurrentUser.UserId;

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.CreateAsync(
                new CreateUserDto(dto.FirstName, dto.LastName, dto.UserName, dto.Password, null,
                    UserRole.PlatformAdmin),
                currentUserId,
                cancellationToken);

            var admin = new PlatformAdmin(userDto.Id, currentUserId);
            await platformAdminRepository.AddAsync(admin, cancellationToken);
            await platformAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new PlatformAdminDto(admin.Id, userDto.FirstName, userDto.LastName, userDto.UserName);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    public async Task<PlatformAdminDto> UpdateAsync( UpdatePlatformAdminDto dto,
        CancellationToken cancellationToken = default)
    {
        var platformAdmin = await GetCurrentEndUserAsync(cancellationToken);
        var currentUserId = currentUserService.CurrentUser.UserId;
      
        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.UpdateAsync(platformAdmin.UserId,
                new UpdateUserDto(dto.FirstName, dto.LastName, dto.UserName, null),
                currentUserId,
                cancellationToken);

            platformAdmin.Modified(currentUserId);
            platformAdminRepository.Update(platformAdmin);
            await platformAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new PlatformAdminDto(platformAdmin.Id, userDto.FirstName, userDto.LastName, userDto.UserName);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    public async Task<PlatformAdminDto> DeleteAsync(long platformAdminId,CancellationToken cancellationToken = default)
    {
        var platformAdmin = await platformAdminRepository.GetByIdAsync(platformAdminId, cancellationToken) ??
                            throw new EntityNotFoundException(nameof(PlatformAdmin), platformAdminId);
        var currentUserId = currentUserService.CurrentUser.UserId;
        if (platformAdmin.UserId == currentUserId)
            throw new CannotDeleteOwnAccountException();

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            platformAdmin.SoftDelete(currentUserId);
            platformAdminRepository.Update(platformAdmin);
            await platformAdminRepository.SaveChangesAsync(cancellationToken);

            var userDto = await userService.DeleteAsync(platformAdmin.UserId, currentUserId, cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new PlatformAdminDto(platformAdmin.Id, userDto.FirstName, userDto.LastName, userDto.UserName);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    private async Task<PlatformAdmin> GetCurrentEndUserAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.CurrentUser.UserId;
        return await platformAdminRepository.GetByUserIdAsync(userId, cancellationToken)
               ?? throw new EntityNotFoundException(nameof(EndUser), userId);
    }

    private static PlatformAdminDto ToDto(PlatformAdmin admin, User user)
        => new(
            admin.Id, user.FirstName, user.LastName, user.UserName
        );
}