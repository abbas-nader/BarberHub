using BarberHub.Application.DTOs.SalonAdmin;
using BarberHub.Application.DTOs.User;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services.Implements;

public class SalonAdminService(
    ISalonAdminRepository salonAdminRepository,
    IUserRepository userRepository,
    ISalonRepository salonRepository,
    IUserService userService,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : ISalonAdminService
{
    public async Task<IReadOnlyList<SalonAdminDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmins = await salonAdminRepository.GetAllBySalonIdAsync(salonId, cancellationToken);
        var users = await userRepository.GetByIdsAsync(salonAdmins.Select(x => x.UserId), cancellationToken);
        return salonAdmins.Select(b => ToDto(b, users[b.UserId])).ToList();
    }

    public async Task<SalonAdminDto> GetByIdAsync(long salonAdminId, CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminRepository.GetByIdAsync(salonAdminId, cancellationToken) ??
                         throw new EntityNotFoundException(nameof(SalonAdmin), salonAdminId);
        var user = await userRepository.GetByIdAsync(salonAdmin.UserId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), salonAdmin.UserId);
        return ToDto(salonAdmin, user);
    }

    public async Task<SalonAdminDto> CreateAsync(CreateSalonAdminDto dto, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(dto.SalonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), dto.SalonId);

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.CreateAsync(
                new CreateUserDto(dto.FirstName, dto.LastName, dto.Username, dto.Password, dto.MobileNumber,
                    UserRole.SalonAdmin),
                currentUserService.CurrentUser.UserId,
                cancellationToken);

            var salonAdmin = new SalonAdmin(userDto.Id, salon.Id, currentUserService.CurrentUser.UserId);
            await salonAdminRepository.AddAsync(salonAdmin, cancellationToken);
            await salonAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new SalonAdminDto(salonAdmin.Id, userDto.FirstName, userDto.LastName, userDto.MobileNumber!);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    public async Task<SalonAdminDto> UpdateAsync(long salonAdminId, UpdateSalonAdminDto dto,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin = await EnsureOwnedAsync(salonAdminId, cancellationToken);

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.UpdateAsync(salonAdmin.UserId,
                new UpdateUserDto(dto.FirstName, dto.LastName, dto.Username, dto.MobileNumber),
                currentUserService.CurrentUser.UserId,
                cancellationToken);

            salonAdmin.Modified(currentUserService.CurrentUser.UserId);
            salonAdminRepository.Update(salonAdmin);
            await salonAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new SalonAdminDto(salonAdmin.Id, userDto.FirstName, userDto.LastName, userDto.MobileNumber!);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    public async Task<SalonAdminDto> DeleteAsync(long salonAdminId, CancellationToken cancellationToken = default)
    {
        var salonAdmin = await EnsureOwnedAsync(salonAdminId, cancellationToken);
        var currentUserId = currentUserService.CurrentUser.UserId;
        if (salonAdmin.UserId == currentUserId)
            throw new CannotDeleteOwnAccountException();
        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            salonAdmin.SoftDelete(currentUserService.CurrentUser.UserId);
            await salonAdminRepository.SaveChangesAsync(cancellationToken);
            var userDto = await userService.DeleteAsync(salonAdmin.UserId, currentUserId, cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new SalonAdminDto(salonAdmin.Id, userDto.FirstName, userDto.LastName, userDto.MobileNumber!);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    private async Task<SalonAdmin> EnsureOwnedAsync(long salonAdminId, CancellationToken cancellationToken)
    {
        var salonAdmin = await salonAdminRepository.GetByIdAsync(salonAdminId, cancellationToken) ??
                         throw new EntityNotFoundException(nameof(SalonAdmin), salonAdminId);

        if (currentUserService.CurrentUser.UserRole == UserRole.PlatformAdmin)
            return salonAdmin;

        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        return salonAdmin.SalonId != salonId
            ? throw new EntityNotFoundException(nameof(SalonAdmin), salonAdmin.Id)
            : salonAdmin;
    }

    private static SalonAdminDto ToDto(SalonAdmin salonAdmin, User user)
        => new(
            salonAdmin.Id,
            user.FirstName,
            user.LastName,
            user.MobileNumber!
        );
}