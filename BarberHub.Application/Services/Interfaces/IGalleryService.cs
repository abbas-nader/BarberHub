using BarberHub.Application.DTOs.Gallery;

namespace BarberHub.Application.Services.InterFaces;

public interface IGalleryService
{
    Task<IReadOnlyList<GalleryDto>> GetAllBySalonIdAsync(long salonId, CancellationToken cancellationToken = default);

    Task<GalleryDto> GetByIdAsync(long galleryId, CancellationToken cancellationToken = default);

    Task<GalleryDto> CreateAsync(CreateGalleryDto createGalleryDto, CancellationToken cancellationToken = default);

    Task<GalleryDto> UpdateAsync(long galleryId, UpdateGalleryCaptionDto updateGalleryCaptionDto,
        CancellationToken cancellationToken = default);

    Task<GalleryDto> DeleteAsync(long galleryId, CancellationToken cancellationToken = default);
}