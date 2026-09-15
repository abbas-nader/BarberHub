using BarberHub.Application.DTOs.SalonAdmin;
using BarberHub.Application.DTOs.Shared;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class SalonAdminService(
    ISalonAdminRepository salonAdminRepository,
    IUserRepository userRepository,
    ISalonRepository salonRepository,
    IPasswordHasher passwordHasher,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
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
        if (await userRepository.ExistsByUserNameAsync(dto.Username, cancellationToken))
            throw new DuplicateUserNameException();

        var salon = await salonRepository.GetByIdAsync(dto.SalonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), dto.SalonId);
        var passwordHash = passwordHasher.Hash(dto.Password);

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var user = new User(dto.FirstName, dto.LastName, dto.Username, passwordHash, UserRole.SalonAdmin,
                dto.MobileNumber, currentUserService.CurrentUser.UserId);
            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);

            var salonAdmin = new SalonAdmin(user.Id, salon.Id, currentUserService.CurrentUser.UserId);
            await salonAdminRepository.AddAsync(salonAdmin, cancellationToken);
            await salonAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return ToDto(salonAdmin, user);
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

        var user = await userRepository.GetByIdAsync(salonAdmin.UserId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), salonAdmin.UserId);

        if (!string.Equals(user.UserName, dto.Username, StringComparison.Ordinal))
        {
            if (await userRepository.ExistsByUserNameAsync(dto.Username, cancellationToken))
                throw new DuplicateUserNameException();
        }

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            user.Update(dto.FirstName, dto.LastName, dto.Username, dto.MobileNumber,
                currentUserService.CurrentUser.UserId);
            userRepository.Update(user);
            await userRepository.SaveChangesAsync(cancellationToken);

            salonAdmin.Modified(currentUserService.CurrentUser.UserId);
            salonAdminRepository.Update(salonAdmin);
            await salonAdminRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return ToDto(salonAdmin, user);
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
        salonAdmin.SoftDelete(currentUserService.CurrentUser.UserId);
        await salonAdminRepository.SaveChangesAsync(cancellationToken);
        var user = await userRepository.GetByIdAsync(salonAdmin.UserId, cancellationToken)??
                   throw new EntityNotFoundException(nameof(User), salonAdmin.UserId);
        return ToDto(salonAdmin, user);
    }

    public async Task<SalonAdminDto> ChangePasswordAsync(ChangePasswordDto dto,
        CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.CurrentUser.UserId;
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), userId);

        if (!passwordHasher.Verify(dto.OldPassword, user.PasswordHash))
            throw new InvalidCurrentPasswordException();

        user.ChangePassword(passwordHasher.Hash(dto.NewPassword), userId);
        await userRepository.SaveChangesAsync(cancellationToken);

        var salonAdmin = await salonAdminRepository.GetByUserIdAsync(userId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(SalonAdmin), userId);
        return ToDto(salonAdmin, user);
    }

    private async Task<SalonAdmin> EnsureOwnedAsync(long salonAdminId, CancellationToken cancellationToken)
    {
        var salonAdmin = await salonAdminRepository.GetByIdAsync(salonAdminId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(SalonAdmin), salonAdminId);
        
        if (currentUserService.CurrentUser.UserRole == UserRole.PlatformAdmin)
            return salonAdmin;
        
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        return salonAdmin.SalonId != salonId ? throw new EntityNotFoundException(nameof(SalonAdmin), salonAdmin.Id) : salonAdmin;
    }

    private static SalonAdminDto ToDto(SalonAdmin salonAdmin, User user)
        => new(
            salonAdmin.Id,
            user.FirstName,
            user.LastName,
            user.MobileNumber!
        );
}