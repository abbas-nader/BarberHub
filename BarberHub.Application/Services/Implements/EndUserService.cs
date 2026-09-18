using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.DTOs.EndUser;
using BarberHub.Application.DTOs.User;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services.Implements;

public class EndUserService(
    IEndUserRepository endUserRepository,
    IUserRepository userRepository,
    IUserService userService,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IEndUserService
{
    public async Task<IReadOnlyList<EndUserDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var endUsers = await endUserRepository.GetAllAsync(cancellationToken);
        var users = await userRepository.GetByIdsAsync(endUsers.Select(x => x.UserId), cancellationToken);
        return endUsers.Select(x => ToDto(x, users[x.UserId])).ToList();
    }

    public async Task<EndUserDto> GetByIdAsync(long endUserId, CancellationToken cancellationToken = default)
    {
        var endUser = await endUserRepository.GetByIdAsync(endUserId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(EndUser), endUserId);
        var user = await userRepository.GetByIdAsync(endUser.UserId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), endUser.UserId);
        return ToDto(endUser, user);
    }

    public async Task<EndUserDto> UpdateAsync(UpdateEndUserDto updateEndUserDto,
        CancellationToken cancellationToken = default)
    {
        var endUser = await GetCurrentEndUserAsync(cancellationToken);
        var currentUserId = currentUserService.CurrentUser.UserId;

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.UpdateAsync(endUser.UserId,
                new UpdateUserDto(updateEndUserDto.FirstName, updateEndUserDto.LastName,
                    updateEndUserDto.UserName, updateEndUserDto.MobileNumber),
                currentUserId,
                cancellationToken);

            endUser.Modified(currentUserId);
            endUserRepository.Update(endUser);
            await endUserRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new EndUserDto(endUser.Id, userDto.FirstName, userDto.LastName, userDto.UserName,
                userDto.MobileNumber, userDto.IsMobileVerified);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    public async Task<EndUserDto> DeleteAsync(CancellationToken cancellationToken = default)
    {
        var endUser = await GetCurrentEndUserAsync(cancellationToken);
        var currentUserId = currentUserService.CurrentUser.UserId;

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            endUser.SoftDelete(currentUserId);
            endUserRepository.Update(endUser);
            await endUserRepository.SaveChangesAsync(cancellationToken);

            var userDto = await userService.DeleteAsync(endUser.UserId, currentUserId, cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new EndUserDto(endUser.Id, userDto.FirstName, userDto.LastName, userDto.UserName,
                userDto.MobileNumber, userDto.IsMobileVerified);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }
    private async Task<EndUser> GetCurrentEndUserAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.CurrentUser.UserId;
        return await endUserRepository.GetByUserIdAsync(userId, cancellationToken)
               ?? throw new EntityNotFoundException(nameof(EndUser), userId);
    }
    private static EndUserDto ToDto(EndUser endUser, User user)
        => new(
            endUser.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.MobileNumber,
            user.IsMobileVerified
        );
}