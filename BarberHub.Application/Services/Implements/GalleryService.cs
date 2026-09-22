using BarberHub.Application.DTOs.File;
using BarberHub.Application.DTOs.Gallery;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Authentication;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace BarberHub.Application.Services.Implements;

public class GalleryService(
    IGalleryRepository galleryRepository,
    IFileService fileService,
    IBarberRepository barberRepository,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork,
    ILogger<GalleryService> logger) : IGalleryService
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

        if (createGalleryDto.BarberId is not null)
        {
            var barber = await barberRepository.GetByIdAsync(createGalleryDto.BarberId.Value, cancellationToken);
            if (barber is null || barber.SalonId != salonId)
                throw new EntityNotFoundException(nameof(Barber), createGalleryDto.BarberId.Value);
        }

        var uploadFileDto = new UploadFileDto(
            createGalleryDto.FileStream,
            createGalleryDto.OriginFileName,
            createGalleryDto.ContentType,
            createGalleryDto.Size
        );
        var uploadedFile = await fileService.UploadAsync(uploadFileDto, cancellationToken);
        try
        {
            await unitOfWork.BeginTransaction(cancellationToken);
            var gallery = new Gallery(createGalleryDto.Caption, salonId, createGalleryDto.BarberId,
                uploadedFile.Id, currentUserService.CurrentUser.UserId);

            await galleryRepository.AddAsync(gallery, cancellationToken);
            await galleryRepository.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction(cancellationToken);
            return ToDto(gallery);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            await fileService.DeleteAsync(uploadedFile.Id, cancellationToken);
            throw;
        }
    }

    public async Task<GalleryDto> UpdateAsync(long galleryId, UpdateGalleryCaptionDto updateGalleryCaptionDto,
        CancellationToken cancellationToken = default)
    {
        var gallery = await galleryRepository.GetByIdAsync(galleryId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(Gallery), galleryId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (gallery.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Gallery), galleryId);
        gallery.UpdateCaption(updateGalleryCaptionDto.Caption, currentUserService.CurrentUser.UserId);
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
        await unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            gallery.SoftDelete(currentUserService.CurrentUser.UserId);
            await galleryRepository.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransaction(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransaction(cancellationToken);
            throw;
        }

        try
        {
            await fileService.DeleteAsync(gallery.FileId, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to delete physical file for gallery {GalleryId}. Needs manual cleanup.",
                galleryId);
        }

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