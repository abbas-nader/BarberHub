using BarberHub.Application.DTOs.Salon;
using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
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

    public async Task<SalonDto> GetById(long id, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(id, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), id);

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

    public async Task<SalonDto> UpdateAsync(UpdateSalonDto updateSalonDto, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(updateSalonDto.Id, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), updateSalonDto.Id);
        salon.UpdateInfo(updateSalonDto.Name, updateSalonDto.Address, updateSalonDto.City, updateSalonDto.PhoneNumber,
            updateSalonDto.Description, modifiedBy);
        salonRepository.Update(salon);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> DeleteAsync(long id, long modifiedBy, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(id, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), id);
        salon.SoftDelete(modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> ActivateAsync(long id, long modifiedBy, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(id, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Barber), id);

        salon.Activate(modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> DeactivateAsync(long id, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(id, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Barber), id);
        salon.Deactivate(modifiedBy);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> UpdateDepositAmount(long id, decimal depositAmountValue, Currency depositAmountCurrency,
        long modifiedBy,
        CancellationToken cancellationToken)
    {
        var salon = await salonRepository.GetByIdAsync(id, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Barber), id);
        salon.UpdateDepositAmount(new Money(depositAmountValue, depositAmountCurrency), modifiedBy);
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