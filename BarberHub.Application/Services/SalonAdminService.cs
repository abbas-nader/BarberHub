using BarberHub.Application.DTOs.Barber;
using BarberHub.Application.DTOs.SalonAdmin;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security;
using BarberHub.Application.Security.Hash;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class SalonAdminService(ISalonAdminRepository salonAdminRepository, IPasswordHasher passwordHasher)
{
    public async Task<IReadOnlyList<SalonAdminDto>> GetAllBySalonIdAsync(long salonId,
        CancellationToken cancellationToken = default)
    {
        var salonAdmins = await salonAdminRepository.GetAllBySalonIdAsync(salonId,
            cancellationToken);
        return salonAdmins.Select(ToDto).ToList();
    }

    public async Task<SalonAdminDto> GetByIdAsync(long salonAdminId, CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminRepository.GetByIdAsync(salonAdminId, cancellationToken) ??
                         throw new EntityNotFoundException(nameof(SalonAdmin), salonAdminId);
        return ToDto(salonAdmin);
    }

    public async Task<SalonAdminDto> CreateAsync(CreateSalonAdminDto createSalonAdminDto, long creationBy,
        CancellationToken cancellationToken = default)
    {
        var checkUserName =
            await salonAdminRepository.ExistsByUserNameAsync(createSalonAdminDto.Username, cancellationToken);
        if (checkUserName)
            throw new DuplicateUserNameException();
        var passwordHash = passwordHasher.Hash(createSalonAdminDto.Password);
        var salonAdmin = new SalonAdmin(createSalonAdminDto.FirstName, createSalonAdminDto.LastName,
            createSalonAdminDto.Username, passwordHash, createSalonAdminDto.MobileNumber, createSalonAdminDto.SalonId,
            creationBy);
        await salonAdminRepository.AddAsync(salonAdmin, cancellationToken);
        await salonAdminRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salonAdmin);
    }

    public async Task<SalonAdminDto> UpdateAsync(long salonAdminId,UpdateSalonAdminDto updateSalonAdminDto,long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminRepository.GetByIdAsync(salonAdminId, cancellationToken);
        if (salonAdmin == null)
            throw new EntityNotFoundException(nameof(SalonAdmin), salonAdminId);
        var checkUserName =
            await salonAdminRepository.ExistsByUserNameAsync(updateSalonAdminDto.Username, cancellationToken);
        if (checkUserName)
            throw new DuplicateUserNameException();
        var passwordHash = string.IsNullOrWhiteSpace(updateSalonAdminDto.Password)
            ? salonAdmin.PasswordHash
            : passwordHasher.Hash(updateSalonAdminDto.Password);
        salonAdmin.Update(updateSalonAdminDto.FirstName, updateSalonAdminDto.LastName, updateSalonAdminDto.Username,
            passwordHash, updateSalonAdminDto.MobileNumber, modifiedBy);
        salonAdminRepository.Update(salonAdmin);
        await salonAdminRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salonAdmin);
    }

    public async Task<SalonAdminDto> DeleteAsync(long salonAdminId,long deletedBy, CancellationToken cancellationToken = default)
    {
        var salonAdmin = await salonAdminRepository.GetByIdAsync(salonAdminId, cancellationToken);
        if (salonAdmin == null)
            throw new EntityNotFoundException(nameof(SalonAdmin), salonAdminId);
        salonAdmin.SoftDelete(deletedBy);
        await salonAdminRepository.SaveChangesAsync(cancellationToken);
        return ToDto(salonAdmin);
    }

    private static SalonAdminDto ToDto(SalonAdmin salonAdmin)
        => new(
            salonAdmin.Id,
            salonAdmin.FirstName,
            salonAdmin.LastName,
            salonAdmin.MobileNumber
        );
}