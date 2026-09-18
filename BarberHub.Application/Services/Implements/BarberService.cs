using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.DTOs.User;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services.Implements;

public class BarberService(
    IBarberRepository barberRepository,
    IUserRepository userRepository,
    IUserService userService,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IBarberService
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
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.CreateAsync(
                new CreateUserDto(dto.FirstName, dto.LastName, dto.UserName, dto.Password, dto.MobileNumber,
                    UserRole.Barber),
                currentUserService.CurrentUser.UserId,
                cancellationToken);

            var barber = new Barber(dto.Description, userDto.Id, salonId, currentUserService.CurrentUser.UserId);
            await barberRepository.AddAsync(barber, cancellationToken);
            await barberRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new BarberDto(barber.Id, userDto.FirstName, userDto.LastName, userDto.MobileNumber!,
                barber.Description, barber.IsActive);
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

        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var userDto = await userService.UpdateAsync(barber.UserId,
                new UpdateUserDto(dto.FirstName, dto.LastName, dto.Username, dto.MobileNumber),
                currentUserService.CurrentUser.UserId,
                cancellationToken);

            barber.Update(dto.Description, currentUserService.CurrentUser.UserId);
            barberRepository.Update(barber);
            await barberRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new BarberDto(barber.Id, userDto.FirstName, userDto.LastName, userDto.MobileNumber!,
                barber.Description, barber.IsActive);
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
        var currentUserId = currentUserService.CurrentUser.UserId;
        
        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            await userService.DeleteAsync(barber.UserId, currentUserService.CurrentUser.UserId, cancellationToken);
            barber.SoftDelete(currentUserService.CurrentUser.UserId);
            await barberRepository.SaveChangesAsync(cancellationToken);
            var userDto = await userService.DeleteAsync(barber.UserId, currentUserId, cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return new BarberDto(barber.Id, userDto.FirstName, userDto.LastName, userDto.MobileNumber!,
                barber.Description, barber.IsActive);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }
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