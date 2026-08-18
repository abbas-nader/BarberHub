using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class AppointmentRepository(BarberHubDbContext context)
    : BaseRepository<Appointment>(context), IAppointmentRepository
{
}