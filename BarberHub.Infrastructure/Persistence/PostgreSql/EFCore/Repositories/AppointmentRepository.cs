using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class AppointmentRepository(BarberHubDbContext context)
    : BaseRepository<Appointment>(context), IAppointmentRepository
{
    public async Task<IReadOnlyList<Appointment>> GetAllByBarberIdAsync(long barberId,
        CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Appointments.Where(x => x.BarberId == barberId && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Appointment>> GetConfirmedByBarberIdAndDateAsync(long barberId, DateOnly date, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Appointments
            .Where(x => x.BarberId == barberId
                        && x.AppointmentDate == date
                        && x.AppointmentStatus == AppointmentStatus.Confirmed
                        && x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}