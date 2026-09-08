using BarberHub.Application.DTOs.WorkSchedule;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Application.Services;

public class WorkScheduleService(
    IWorkScheduleRepository workScheduleRepository,
    ICurrentUserService currentUserService,
    IBarberRepository barberRepository)
{
    public async Task<IReadOnlyList<WorkScheduleDto>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleRepository.GetAllByBarberIdAsync(barberId, cancellationToken);
        return workSchedule.Select(ToDto).ToList();
    }

    public async Task<WorkScheduleDto> GetByIdAsync(long workScheduleId, CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleRepository.GetByIdAsync(workScheduleId, cancellationToken) ??
                           throw new EntityNotFoundException(nameof(WorkSchedule), workScheduleId);
        return ToDto(workSchedule);
    }

    public async Task<WorkScheduleDto> CreateAsync(CreateWorkScheduleDto createWorkScheduleDto,
        CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barber = await barberRepository.GetByIdAsync(createWorkScheduleDto.BarberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), createWorkScheduleDto.BarberId);
        var workSchedule = new WorkSchedule(
            createWorkScheduleDto.StartTime, createWorkScheduleDto.EndTime, createWorkScheduleDto.DayOfWeek,
            createWorkScheduleDto.BarberId, currentUserService.CurrentUser.UserId);
        await workScheduleRepository.AddAsync(workSchedule, cancellationToken);
        await workScheduleRepository.SaveChangesAsync(cancellationToken);
        return ToDto(workSchedule);
    }

    public async Task<WorkScheduleDto> UpdateAsync(long workScheduleId, UpdateWorkScheduleDto updateWorkScheduleDto,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleRepository.GetByIdAsync(workScheduleId, cancellationToken) ??
                           throw new EntityNotFoundException(nameof(WorkSchedule), workScheduleId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barber = await barberRepository.GetByIdAsync(workSchedule.BarberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), workSchedule.BarberId);
        workSchedule.Update(updateWorkScheduleDto.StartTime, updateWorkScheduleDto.EndTime,
            updateWorkScheduleDto.DayOfWeek, currentUserService.CurrentUser.UserId);
        workScheduleRepository.Update(workSchedule);
        await workScheduleRepository.SaveChangesAsync(cancellationToken);
        return ToDto(workSchedule);
    }

    public async Task<WorkScheduleDto> DeleteAsync(long workScheduleId, CancellationToken cancellationToken = default)
    {
        var workSchedule = await workScheduleRepository.GetByIdAsync(workScheduleId, cancellationToken);
        if (workSchedule == null)
            throw new EntityNotFoundException(nameof(WorkSchedule), workScheduleId);
        var salonId = currentUserService.CurrentUser.SalonId
                      ?? throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barber = await barberRepository.GetByIdAsync(workSchedule.BarberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Barber), workSchedule.BarberId);
        workSchedule.SoftDelete(currentUserService.CurrentUser.UserId);
        await workScheduleRepository.SaveChangesAsync(cancellationToken);
        return ToDto(workSchedule);
    }

    private static WorkScheduleDto ToDto(WorkSchedule workSchedule)
        => new(
            workSchedule.Id,
            workSchedule.StartTime,
            workSchedule.EndTime,
            workSchedule.DayOfWeek,
            workSchedule.BarberId
        );
}