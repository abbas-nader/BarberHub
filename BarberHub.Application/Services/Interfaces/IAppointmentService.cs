using BarberHub.Application.DTOs.Appointment;

namespace BarberHub.Application.Services.InterFaces;

public interface IAppointmentService
{
    Task<IReadOnlyList<AppointmentDto>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default);

    Task<AppointmentDto> GetByIdAsync(long appointmentId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AvailableSlotDto>> GetAvailableSlotsAsync(long barberServiceId, DateOnly date,
        CancellationToken cancellationToken = default);

    Task<AppointmentDto> CreateAsync(CreatAppointmentDto createAppointmentDto,
        CancellationToken cancellationToken = default);

    Task<AppointmentDto> CancelByUserAsync(long appointmentId, CancellationToken cancellationToken = default);

    Task<AppointmentDto> CancelBySalonAsync(long appointmentId, CancellationToken cancellationToken = default);

    Task<AppointmentDto> CompleteAsync(long appointmentId, CancellationToken cancellationToken = default);
}