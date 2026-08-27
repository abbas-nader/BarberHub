using BarberHub.Application.DTOs;
using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class BarberService(IBarberRepository barberRepository, IPasswordHasher passwordHasher)
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

    public async Task<BarberDto> CreateAsync(CreateBarberDto createBarberDto, long creationBy,
        CancellationToken cancellationToken = default)
    {
        var checkUserName =
            await barberRepository.ExistsByUserNameAsync(createBarberDto.Username, cancellationToken);
        if (checkUserName)
            throw new DuplicateUserNameException();

        var passwordHash = passwordHasher.Hash(createBarberDto.Password);
        var barber = new Barber(createBarberDto.FirstName, createBarberDto.LastName, createBarberDto.MobileNumber,
            createBarberDto.Username, passwordHash, createBarberDto.Description, createBarberDto.SalonId,
            creationBy);

        await barberRepository.AddAsync(barber, cancellationToken);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> UpdateAsync(long barberId, UpdateBarberDto updateBarberDto, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        var checkUserName = await barberRepository.ExistsByUserNameAsync(updateBarberDto.Username, cancellationToken);
        if (checkUserName)
            throw new DuplicateUserNameException();
        var passwordHash = string.IsNullOrWhiteSpace(updateBarberDto.Password)
            ? barber.PasswordHash
            : passwordHasher.Hash(updateBarberDto.Password);

        barber.Update(updateBarberDto.FirstName, updateBarberDto.LastName, updateBarberDto.MobileNumber,
            updateBarberDto.Username, passwordHash, updateBarberDto.Description, modifiedBy);
        barberRepository.Update(barber);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> DeleteAsync(long barberId, long deletedBy,
        CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        barber.SoftDelete(deletedBy);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> ActivateAsync(long barberId, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);

        barber.Activate(modifiedBy);
        await barberRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barber);
    }

    public async Task<BarberDto> DeactivateAsync(long barberId, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber == null)
            throw new EntityNotFoundException(nameof(Barber), barberId);
        barber.Deactivate(modifiedBy);
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