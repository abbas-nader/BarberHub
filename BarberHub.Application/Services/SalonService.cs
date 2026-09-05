using BarberHub.Application.DTOs.Salon;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Services;

public class SalonService(ISalonRepository salonRepository, ICurrentUserService currentUserService)
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

    public async Task<SalonDto> CreateAsync(CreateSalonDto createSalonDto,
        CancellationToken cancellationToken = default)
    {
        var salon = new Salon(createSalonDto.Name, createSalonDto.Address, createSalonDto.City,
            createSalonDto.PhoneNumber,
            new Money(createSalonDto.DepositAmountValue, createSalonDto.DepositAmountCurrency),
            createSalonDto.Description, currentUserService.CurrentUser.UserId);
        await salonRepository.AddAsync(salon, cancellationToken);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> UpdateAsync(UpdateSalonDto updateSalonDto,
        CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), salonId);
        salon.UpdateInfo(updateSalonDto.Name, updateSalonDto.Address, updateSalonDto.City, updateSalonDto.PhoneNumber,
            updateSalonDto.Description, currentUserService.CurrentUser.UserId);
        salonRepository.Update(salon);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> DeleteAsync(long salonId, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), salonId);
        salon.SoftDelete(currentUserService.CurrentUser.UserId);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> ActivateAsync(long salonId, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Salon), salonId);

        salon.Activate(currentUserService.CurrentUser.UserId);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> DeactivateAsync(long salonId, CancellationToken cancellationToken = default)
    {
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Salon), salonId);
        salon.Deactivate(currentUserService.CurrentUser.UserId);
        await salonRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salon);
    }

    public async Task<SalonDto> UpdateDepositAmount(
        UpdateSalonDepositAmountDto updateSalonDepositAmountDto, CancellationToken cancellationToken)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var salon = await salonRepository.GetByIdAsync(salonId, cancellationToken);
        if (salon == null)
            throw new EntityNotFoundException(nameof(Salon), salonId);
        salon.UpdateDepositAmount(
            new Money(updateSalonDepositAmountDto.DepositAmountValue, updateSalonDepositAmountDto.Currency),
            currentUserService.CurrentUser.UserId);
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