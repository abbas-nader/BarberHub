using BarberHub.Application.DTOs.Gallery;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Application.Services;

public class GalleryService(
    IGalleryRepository galleryRepository,
    IFileRepository fileRepository,
    IBarberRepository barberRepository,
    ICurrentUserService currentUserService)
{
    public async Task<IReadOnlyList<GalleryDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        var galleries = await galleryRepository.GetAllBySalonIdAsync(salonId, cancellationToken);
        return galleries.Select(ToDto).ToList();
    }

    public async Task<GalleryDto> GetByIdAsync(long galleryId, CancellationToken cancellationToken = default)
    {
        var gallery = await galleryRepository.GetByIdAsync(galleryId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(Gallery), galleryId);
        return ToDto(gallery);
    }

    public async Task<GalleryDto> CreateAsync(CreateGalleryDto createGalleryDto,
        CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));

        var file = await fileRepository.GetByIdAsync(createGalleryDto.FileId, cancellationToken);

        if (file is null)
            throw new EntityNotFoundException(nameof(File), createGalleryDto.FileId);
        if (createGalleryDto.BarberId is not null)
        {
            var barber = await barberRepository.GetByIdAsync(createGalleryDto.BarberId.Value, cancellationToken);
            if (barber is null || barber.SalonId != salonId)
                throw new EntityNotFoundException(nameof(Barber), createGalleryDto.BarberId.Value);
        }

        var gallery = new Gallery(createGalleryDto.Caption, salonId, createGalleryDto.BarberId,
            createGalleryDto.FileId, currentUserService.CurrentUser.UserId);

        await galleryRepository.AddAsync(gallery, cancellationToken);
        await galleryRepository.SaveChangesAsync(cancellationToken);
        return ToDto(gallery);
    }

    public async Task<GalleryDto> UpdateAsync(long galleryId, UpdateCaptionDto updateCaptionDto,
        CancellationToken cancellationToken = default)
    {
        var gallery = await galleryRepository.GetByIdAsync(galleryId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(Gallery), galleryId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (gallery.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Gallery), galleryId);
        gallery.UpdateCaption(updateCaptionDto.Caption, currentUserService.CurrentUser.UserId);
        galleryRepository.Update(gallery);
        await galleryRepository.SaveChangesAsync(cancellationToken);
        return ToDto(gallery);
    }

    public async Task<GalleryDto> DeleteAsync(long galleryId, CancellationToken cancellationToken = default)
    {
        var gallery = await galleryRepository.GetByIdAsync(galleryId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(Gallery), galleryId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (gallery.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Gallery), galleryId);
        gallery.SoftDelete(currentUserService.CurrentUser.UserId);
        await galleryRepository.SaveChangesAsync(cancellationToken);
        return ToDto(gallery);
    }

    private static GalleryDto ToDto(Gallery gallery)
        => new(
            gallery.Id,
            gallery.Caption,
            gallery.SalonId,
            gallery.BarberId,
            gallery.FileId
        );
}