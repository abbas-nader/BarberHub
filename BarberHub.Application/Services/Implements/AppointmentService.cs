using BarberHub.Application.DTOs.Appointment;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Authentication;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Services.Implements;

public class AppointmentService(
    IAppointmentRepository appointmentRepository,
    IBarberServiceRepository barberServiceRepository,
    IBarberRepository barberRepository,
    IServiceRepository serviceRepository,
    ISalonRepository salonRepository,
    IEndUserRepository endUserRepository,
    ICurrentUserService currentUserService,
    IWalletTransactionRepository walletTransactionRepository,
    IWalletTransactionService walletTransactionService, 
    IWorkScheduleRepository workScheduleRepository) : IAppointmentService
{
    public async Task<IReadOnlyList<AppointmentDto>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default)
    {
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        var barber = await barberRepository.GetByIdAsync(barberId, cancellationToken);
        if (barber is null || barber.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Appointment), barberId);
        var appointments = await appointmentRepository.GetAllByBarberIdAsync(barberId, cancellationToken);
        return appointments.Select(ToDto).ToList();
    }

    public async Task<AppointmentDto> GetByIdAsync(long appointmentId, CancellationToken cancellationToken = default)
    {
        var appointment = await appointmentRepository.GetByIdAsync(appointmentId, cancellationToken) ??
                          throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        return ToDto(appointment);
    }

    public async Task<IReadOnlyList<AvailableSlotDto>> GetAvailableSlotsAsync(long barberServiceId, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var barberService = await barberServiceRepository.GetByIdAsync(barberServiceId, cancellationToken) ??
                            throw new EntityNotFoundException(nameof(BarberService), barberServiceId);

        var barber = await barberRepository.GetByIdAsync(barberService.BarberId, cancellationToken);
        if (barber is null || !barber.IsActive)
            throw new EntityNotFoundException(nameof(Barber), barberService.BarberId);

        var workSchedules = await workScheduleRepository.GetAllByBarberIdAsync(barber.Id, cancellationToken);
        var todaySchedules = workSchedules.Where(x => x.DayOfWeek == date.DayOfWeek).ToList();
        if (todaySchedules.Count == 0)
            return [];

        var confirmedAppointments =
            await appointmentRepository.GetConfirmedByBarberIdAndDateAsync(barber.Id, date, cancellationToken);

        var duration = barberService.Duration;
        var nowInIran = DateTimeOffset.UtcNow.ToOffset(TimeZoneConstants.IranOffset);
        var isToday = date == DateOnly.FromDateTime(nowInIran.DateTime);
        var nowTime = TimeOnly.FromDateTime(nowInIran.DateTime);

        var slots = new List<AvailableSlotDto>();

        foreach (var schedule in todaySchedules)
        {
            var slotStart = schedule.StartTime;
            while (slotStart.Add(duration) <= schedule.EndTime)
            {
                var slotEnd = slotStart.Add(duration);
                var isPast = isToday && slotStart <= nowTime;
                var hasConflict = confirmedAppointments.Any(a => slotStart < a.EndTime && a.StartTime < slotEnd);

                if (!isPast && !hasConflict)
                    slots.Add(new AvailableSlotDto(slotStart, slotEnd));

                slotStart = slotEnd;
            }
        }

        return slots;
    }

    public async Task<AppointmentDto> CreateAsync(CreatAppointmentDto createAppointmentDto,
        CancellationToken cancellationToken = default)
    {
        var barberService =
            await barberServiceRepository.GetByIdAsync(createAppointmentDto.BarberServiceId, cancellationToken) ??
            throw new EntityNotFoundException(nameof(BarberService), createAppointmentDto.BarberServiceId);

        var barber = await barberRepository.GetByIdAsync(barberService.BarberId, cancellationToken);
        if (barber is null || !barber.IsActive)
            throw new EntityNotFoundException(nameof(Barber), barberService.BarberId);

        var service = await serviceRepository.GetByIdAsync(barberService.ServiceId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(Service), barberService.ServiceId);

        var salon = await salonRepository.GetByIdAsync(barber.SalonId, cancellationToken) ??
                    throw new EntityNotFoundException(nameof(Salon), barber.SalonId);

        var endUser = await endUserRepository.GetByUserIdAsync(currentUserService.CurrentUser.UserId,
                          cancellationToken) ??
                      throw new EntityNotFoundException(nameof(EndUser), currentUserService.CurrentUser.UserId);
        if (createAppointmentDto.DepositPaymentMethod == DepositPaymentMethod.Wallet)
            throw new NotImplementedException("Wallet payment در حال حاضر پشتیبانی نمی‌شود.");

        var endTime = createAppointmentDto.StartTime.Add(barberService.Duration);

        var serviceSnapshot = new ServiceSnapshot(service.Name, barberService.Duration, barberService.Price);

        var appointment = new Appointment(
            createAppointmentDto.AppointmentDate,
            createAppointmentDto.StartTime,
            endTime,
            serviceSnapshot,
            salon.DepositAmount,
            createAppointmentDto.DepositPaymentMethod,
            barber.Id,
            endUser.Id,
            salon.Id,
            barberService.Id,
            currentUserService.CurrentUser.UserId);

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await appointmentRepository.SaveChangesAsync(cancellationToken);
        return ToDto(appointment);
    }

    public async Task<AppointmentDto> CancelByUserAsync(long appointmentId,
        CancellationToken cancellationToken = default)
    {
        var appointment = await appointmentRepository.GetByIdAsync(appointmentId, cancellationToken) ??
                          throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        var endUser = endUserRepository.GetByIdAsync(currentUserService.CurrentUser.UserId, cancellationToken) ??
                      throw new EntityNotFoundException(nameof(EndUser), currentUserService.CurrentUser.UserId);
        if (appointment.UserId != endUser.Id)
            throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        appointment.CancelByUser(currentUserService.CurrentUser.UserId);
        await appointmentRepository.SaveChangesAsync(cancellationToken);
        return ToDto(appointment);
    }

    public async Task<AppointmentDto> CancelBySalonAsync(long appointmentId,
        CancellationToken cancellationToken = default)
    {
        var appointment = await appointmentRepository.GetByIdAsync(appointmentId, cancellationToken) ??
                          throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (appointment.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        appointment.CancelBySalon(currentUserService.CurrentUser.UserId);
        await appointmentRepository.SaveChangesAsync(cancellationToken);
        return ToDto(appointment);
    }

    public async Task<AppointmentDto> CompleteAsync(long appointmentId, CancellationToken cancellationToken = default)
    {
        var appointment = await appointmentRepository.GetByIdAsync(appointmentId, cancellationToken) ??
                          throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        var salonId = currentUserService.CurrentUser.SalonId ??
                      throw new RequiredClaimMissingException(nameof(TokenClaims.SalonId));
        if (appointment.SalonId != salonId)
            throw new EntityNotFoundException(nameof(Appointment), appointmentId);
        appointment.Complete(currentUserService.CurrentUser.UserId);
        await appointmentRepository.SaveChangesAsync(cancellationToken);
        return ToDto(appointment);
    }

    private static AppointmentDto ToDto(Appointment appointment)
        => new(
            appointment.Id,
            appointment.AppointmentDate,
            appointment.StartTime,
            appointment.EndTime,
            appointment.AppointmentStatus,
            appointment.ServiceSnapshot.ServiceName,
            appointment.ServiceSnapshot.ServiceDuration,
            appointment.ServiceSnapshot.ServicePrice.Value,
            appointment.ServiceSnapshot.ServicePrice.Currency,
            appointment.DepositAmountSnapshot.Value,
            appointment.DepositAmountSnapshot.Currency,
            appointment.DepositPaymentMethod,
            appointment.DepositStatus,
            appointment.CancelledAt,
            appointment.NoShowDetectionType,
            appointment.BarberId,
            appointment.UserId,
            appointment.SalonId,
            appointment.BarberServiceId
        );
}