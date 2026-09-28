using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IReadOnlyList<Appointment>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetConfirmedByBarberIdAndDateAsync(long barberId, DateOnly date,
        CancellationToken cancellationToken = default);
}