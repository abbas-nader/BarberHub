using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Infrastructure;

public class BarberHubDbContext(DbContextOptions<BarberHubDbContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Barber> Barbers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Salon> Salons { get; set; }
    public DbSet<SalonAdmin> SalonAdmins { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<BarberService> BarberServices { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }
}