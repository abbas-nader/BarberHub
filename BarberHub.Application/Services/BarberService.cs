using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.DTOs.Shared;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class BarberService(
    IBarberRepository barberRepository,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork)
{
    public async Task<IReadOnlyList<BarberDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        var barbers = await barberRepository.GetAllBySalonIdAsync(salonId, cancellationToken);
        var users = await userRepository.GetByIdsAsync(barbers.Select(x => x.UserId), cancellationToken);
        return barbers.Select(b => ToDto(b, users[b.UserId])).ToList();
    }

    public async Task<BarberDto> GetByIdAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(Barber), barberId);
        var user = await userRepository.GetByIdAsync(barber.UserId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), barber.UserId);
        return ToDto(barber, user);
    }

    public async Task<BarberDto> CreateAsync(CreateBarberDto dto, CancellationToken cancellationToken = default)
    {
        if (await userRepository.ExistsByUserNameAsync(dto.Username, cancellationToken))
            throw new DuplicateUserNameException();

        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var passwordHash = passwordHasher.Hash(dto.Password);

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var user = new User(dto.FirstName, dto.LastName, dto.Username, passwordHash, UserRole.Barber,
                dto.MobileNumber, currentUserService.CurrentUser.UserId);
            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);

            var barber = new Barber(dto.Description, user.Id, salonId, currentUserService.CurrentUser.UserId);
            await barberRepository.AddAsync(barber, cancellationToken);
            await barberRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return ToDto(barber, user);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }

    public async Task<BarberDto> UpdateAsync(long barberId, UpdateBarberDto dto,
        CancellationToken cancellationToken = default)
    {
        var barber = await EnsureOwnedAsync(barberId, cancellationToken);
        var user = await userRepository.GetByIdAsync(barber.UserId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), barber.UserId);

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

            barber.Update(dto.Description, currentUserService.CurrentUser.UserId);
            barberRepository.Update(barber);
            await barberRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return ToDto(barber, user);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
    }
    public async Task<BarberDto> DeleteAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await EnsureOwnedAsync(barberId, cancellationToken);
        barber.SoftDelete(currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        var user = await userRepository.GetByIdAsync(barber.UserId, cancellationToken)??
                   throw new EntityNotFoundException(nameof(User), barber.UserId);
        return ToDto(barber, user);
    }

    public async Task<BarberDto> ActivateAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await EnsureOwnedAsync(barberId, cancellationToken);
        barber.Activate(currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        var user = await userRepository.GetByIdAsync(barber.UserId, cancellationToken);
        return ToDto(barber, user!);
    }

    public async Task<BarberDto> DeactivateAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await EnsureOwnedAsync(barberId, cancellationToken);
        barber.Deactivate(currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        var user = await userRepository.GetByIdAsync(barber.UserId, cancellationToken);
        return ToDto(barber, user!);
    }

    public async Task<BarberDto> ChangePasswordAsync(ChangePasswordDto dto,
        CancellationToken cancellationToken = default)
    {
        var userId = currentUserService.CurrentUser.UserId;
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(User), userId);

        if (!passwordHasher.Verify(dto.OldPassword, user.PasswordHash))
            throw new InvalidCurrentPasswordException();

        user.ChangePassword(passwordHasher.Hash(dto.NewPassword), userId);
        await userRepository.SaveChangesAsync(cancellationToken);

        var barber = await barberRepository.GetByUserIdAsync(userId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(Barber), userId);
        return ToDto(barber, user);
    }

    private async Task<Barber> EnsureOwnedAsync(long barberId, CancellationToken cancellationToken)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(Barber), barberId);
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        return barber.SalonId != salonId ? throw new EntityNotFoundException(nameof(Barber), barber.Id) : barber;
    }

    private static BarberDto ToDto(Barber barber, User user)
        => new(
            barber.Id,
            user.FirstName,
            user.LastName,
            user.MobileNumber!,
            barber.Description,
            barber.IsActive
        );
}