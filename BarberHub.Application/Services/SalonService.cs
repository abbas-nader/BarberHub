using BarberHub.Application.DTOs.Salon;
using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Services;

public class SalonService(ISalonRepository salonRepository)
{
    public async Task<IReadOnlyList<SalonDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var salons = await salonRepository.GetAllAsync(cancellationToken);
        return salons.Select(ToDto).ToList();
    }

    public async Task<SalonDto> GetById(long salonId, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), salonId);

        return ToDto(salon);
    }

    public async Task<SalonDto> CreateAsync(CreateSalonDto createSalonDto, long creationBy,
        CancellationToken cancellationToken = default)
    {
        var salon = new Salon(createSalonDto.Name, createSalonDto.Address, createSalonDto.City,
            createSalonDto.PhoneNumber,
            new Money(createSalonDto.DepositAmountValue, createSalonDto.DepositAmountCurrency),
            createSalonDto.Description, creationBy);
        await salonRepository.AddAsync(salon, cancellationToken);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> UpdateAsync(long salonId, UpdateSalonDto updateSalonDto, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), salonId);
        salon.UpdateInfo(updateSalonDto.Name, updateSalonDto.Address, updateSalonDto.City, updateSalonDto.PhoneNumber,
            updateSalonDto.Description, modifiedBy);
        salonRepository.Update(salon);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> DeleteAsync(long salonId, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), salonId);
        salon.SoftDelete(modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> ActivateAsync(long salonId, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Barber), salonId);

        salon.Activate(modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> DeactivateAsync(long salonId, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Barber), salonId);
        salon.Deactivate(modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> UpdateDepositAmount(long salonId,
        UpdateSalonDepositAmountDto updateSalonDepositAmountDto,
        long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Barber), salonId);
        salon.UpdateDepositAmount(
            new Money(updateSalonDepositAmountDto.DepositAmountValue, updateSalonDepositAmountDto.Currency),
            modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    private static SalonDto ToDto(Salon salon)
        => new(
            salon.Id,
            salon.Name,
            salon.Address,
            salon.City,
            salon.PhoneNumber,
            salon.DepositAmount.Value,
            salon.DepositAmount.Currency,
            salon.Description,
            salon.IsActive
        );
}