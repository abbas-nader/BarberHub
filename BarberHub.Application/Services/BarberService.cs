using System.Data;
using BarberHub.Application.DTOs;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class BarberService(IBarberRepository barberRepository, IPasswordHasher passwordHasher)
{
    public async Task<IReadOnlyList<BarberDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var barbers = await barberRepository.GetAllAsync(cancellationToken);
        return barbers.Select(ToDto).ToList();
    }

    public async Task<BarberDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var barber = await barberRepository.GetByIdAsync(id, cancellationToken) ??
                     throw new EntityNotFoundException(nameof(Barber), id);
        return ToDto(barber);
    }

    public async Task CreateAsync(CreateBarberDto createBarberDto, long creationsBy,
        CancellationToken cancellationToken = default)
    {
        var checkUserName =
           await barberRepository.ExistsByUserNameAsync(createBarberDto.Username, cancellationToken);
        if (checkUserName)
            throw new DuplicateUserNameException();
        
        var passwordHash = passwordHasher.Hash(createBarberDto.Password);
        var barber = new Barber(createBarberDto.FirstName, createBarberDto.LastName, createBarberDto.MobileNumber,
            createBarberDto.Username, passwordHash, createBarberDto.Description, createBarberDto.SalonId,
            creationsBy);

        await barberRepository.AddAsync(barber, cancellationToken);
        await barberRepository.SaveChangesAsync(cancellationToken);
    }

    public void Update(UpdateBarberDto updateBarberDto, long modifiedBy,
        CancellationToken cancellationToken = default)
    {
        
    }
    
    private static BarberDto ToDto(Barber barber)
        => new BarberDto(
            barber.Id,
            barber.FirstName,
            barber.LastName,
            barber.MobileNumber,
            barber.Description,
            barber.IsActive
        );
}