using System.Security.Authentication;
using BarberHub.Application.DTOs.BarberService;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.JwtToken;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Services;

public class BarberServiceCatalogService(
    IBarberServiceRepository barberServiceRepository,
    IBarberRepository barberRepository,
    IServiceRepository serviceRepository,
    ICurrentUserService currentUserService)
{
    public async Task<IReadOnlyList<BarberServiceDto>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default)
    {
        var barberServices = await barberServiceRepository.GetAllByBarberIdAsync(barberId, cancellationToken);
        return barberServices.Select(ToDto).ToList();
    }

    public async Task<BarberServiceDto> GetByIdAsync(long barberId, CancellationToken cancellationToken = default)
    {
        var barberService = await barberServiceRepository.GetByIdAsync(barberId, cancellationToken) ??
                            throw new EntityNotFoundException(nameof(BarberService), barberId);
        return ToDto(barberService);
    }

    public async Task<BarberServiceDto> CreateAsync(CreateBarberServiceDto createBarberServiceDto,
        CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barber = await barberRepository.GetByIdAsync(createBarberServiceDto.BarberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), createBarberServiceDto.BarberId);

        var service = await serviceRepository.GetByIdAsync(createBarberServiceDto.ServiceId, cancellationToken);
        if (service is null || service.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Service), createBarberServiceDto.ServiceId);
        var barberService = new Domain.Entities.BarberService(createBarberServiceDto.BarberId,
            createBarberServiceDto.ServiceId,
            new Money(createBarberServiceDto.PriceValue, createBarberServiceDto.Currency),
            createBarberServiceDto.Duration,
            currentUserService.CurrentUser.UserId);
        await barberServiceRepository.AddAsync(barberService, cancellationToken);
        await barberServiceRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barberService);
    }

    public async Task<BarberServiceDto> UpdateAsync(long barberServiceId, UpdateBarberServiceDto updateBarberServiceDto,
        CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barberService = await barberServiceRepository.GetByIdAsync(barberServiceId, cancellationToken) ??
                            throw new EntityNotFoundException(nameof(BarberService), barberServiceId);
        var barber = await barberRepository.GetByIdAsync(barberService.BarberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(BarberService), barberServiceId);
        barberService.Update(new Money(updateBarberServiceDto.PriceValue, updateBarberServiceDto.Currency),
            updateBarberServiceDto.Duration, currentUserService.CurrentUser.UserId);
        await barberServiceRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barberService);
    }

    public async Task<BarberServiceDto> DeleteAsync(long barberServiceId, CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barberService = await barberServiceRepository.GetByIdAsync(barberServiceId, cancellationToken) ??
                            throw new EntityNotFoundException(nameof(BarberService), barberServiceId);
        var barber = await barberRepository.GetByIdAsync(barberService.BarberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(BarberService), barberServiceId);
        barberService.SoftDelete(currentUserService.CurrentUser.UserId);
        await barberServiceRepository.SaveChangesAsync(cancellationToken);
        return ToDto(barberService);
    }

    private static BarberServiceDto ToDto(Domain.Entities.BarberService service)
        => new(
            service.Id,
            service.BarberId,
            service.ServiceId,
            service.Price.Value,
            service.Price.Currency,
            service.Duration
        );
}