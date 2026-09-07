using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.DTOs.Shared;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Hash;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class BarberService(
    IBarberRepository barberRepository,
    IPasswordHasher passwordHasher,
    ICurrentUserService currentUserService)
{
    public async Task<IReadOnlyList<BarberDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        var barbers = await barberRepository.GetAllBySalonIdAsync(salonId, cancellationToken);
        return barbers.Select(ToDto).ToList();
    }

    public async Task<BarberDto> GetByIdAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(Barber), barberId);
        return ToDto(barber);
    }

    public async Task<BarberDto> CreateAsync(CreateBarberDto createBarberDto,
        CancellationToken cancellationToken = default)
    {
        var checkUserName =
            await barberRepository.ExistsByUserNameAsync(createBarberDto.Username, cancellationToken);
        if (checkUserName)
            throw new DuplicateUserNameException();

        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));

        var passwordHash = passwordHasher.Hash(createBarberDto.Password);
        var barber = new Barber(createBarberDto.FirstName, createBarberDto.LastName, createBarberDto.MobileNumber,
            createBarberDto.Username, passwordHash, createBarberDto.Description, salonId,
            currentUserService.CurrentUser.UserId);

        await barberRepository.AddAsync(barber, cancellationToken);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> UpdateAsync(long barberId, UpdateBarberDto updateBarberDto,
        CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), barber.Id);

        if (!string.Equals(barber.UserName, updateBarberDto.Username, StringComparison.Ordinal))
        {
            var checkUserName =
                await barberRepository.ExistsByUserNameAsync(updateBarberDto.Username, cancellationToken);
            if (checkUserName)
                throw new DuplicateUserNameException();
        }

        barber.Update(updateBarberDto.FirstName, updateBarberDto.LastName, updateBarberDto.MobileNumber,
            updateBarberDto.Username, updateBarberDto.Description,
            currentUserService.CurrentUser.UserId);
        barberRepository.Update(barber);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> DeleteAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), barber.Id);

        barber.SoftDelete(currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> ActivateAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), barber.Id);

        barber.Activate(currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> DeactivateAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), barber.Id);

        barber.Deactivate(currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto,
        CancellationToken cancellationToken = default)
    {
        var barberId = currentUserService.CurrentUser.UserId;
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(Barber), barberId);

        if (!passwordHasher.Verify(changePasswordDto.OldPassword, barber.PasswordHash))
            throw new InvalidCurrentPasswordException();

        var newPasswordHash = passwordHasher.Hash(changePasswordDto.NewPassword);
        barber.ChangePassword(newPasswordHash, currentUserService.CurrentUser.UserId);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    private static BarberDto ToDto(Barber barber)
        => new(
            barber.Id,
            barber.FirstName,
            barber.LastName,
            barber.MobileNumber,
            barber.Description,
            barber.IsActive
        );
}