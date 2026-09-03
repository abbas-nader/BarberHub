using BarberHub.Application.DTOs.Service;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.JwtToken;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class ServiceCatalogService(IServiceRepository serviceRepository, ICurrentUserService currentUserService)
{
    public async Task<IReadOnlyList<ServiceDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        var service = await serviceRepository.GetAllBySalonIdAsync(salonId, cancellationToken);
        return service.Select(ToDto).ToList();
    }

    public async Task<ServiceDto> GetByIdAsync(long serviceId, CancellationToken cancellationToken = default)
    {
        var service = await serviceRepository.GetByIdAsync(serviceId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(Service), serviceId);
        return ToDto(service);
    }

    public async Task<ServiceDto> CreateAsync(CreateServiceDto service, CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var newService = new Service(service.Name, service.Description, service.Duration, salonId,
            currentUserService.CurrentUser.UserId);
        await serviceRepository.AddAsync(newService, cancellationToken);
        await serviceRepository.SaveChangesAsync(cancellationToken);
        return ToDto(newService);
    }

    public async Task<ServiceDto> UpdateAsync(long serviceId, UpdateServiceDto service,
        CancellationToken cancellationToken = default)
    {
        var serviceToUpdate = await serviceRepository.GetByIdAsync(serviceId, cancellationToken) ??
                              throw new EntityNotFoundException(nameof(Service), serviceId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (serviceToUpdate.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Service), serviceToUpdate.SalonId);
        serviceToUpdate.UpdateService(service.Name, service.Description, service.Duration,
            currentUserService.CurrentUser.UserId);
        serviceRepository.Update(serviceToUpdate);
        await serviceRepository.SaveChangesAsync(cancellationToken);
        return ToDto(serviceToUpdate);
    }

    public async Task<ServiceDto> DeleteAsync(long serviceId, CancellationToken cancellationToken = default)
    {
        var serviceToDelete = await serviceRepository.GetByIdAsync(serviceId, cancellationToken) ??
                              throw new EntityNotFoundException(nameof(Service), serviceId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (serviceToDelete.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Service), serviceToDelete.SalonId);
        serviceToDelete.SoftDelete(currentUserService.CurrentUser.UserId);
        await serviceRepository.SaveChangesAsync(cancellationToken);
        return ToDto(serviceToDelete);
    }

    private static ServiceDto ToDto(Service service)
        => new(
            service.Id,
            service.Name,
            service.Description,
            service.Duration
        );
}