using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class BarberConfiguration : IEntityTypeConfiguration<Barber>
{
    public void Configure(EntityTypeBuilder<Barber> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.MobileNumber)
            .IsRequired()
            .HasMaxLength(11);
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.Description)
            .HasMaxLength(500);
        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => x.PasswordHash).IsUnique();

        builder.HasOne<Salon>()
            .WithMany(b => b.Barbers)
            .HasForeignKey(m => m.SalonId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(b => b.WorkSchedules)
            .WithOne()
            .HasForeignKey(w => w.BarberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.BarberServices)
            .WithOne()
            .HasForeignKey(bs => bs.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(b => b.Appointments)
            .WithOne()
            .HasForeignKey(bs => bs.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(b => b.Reviews)
            .WithOne()
            .HasForeignKey(bs => bs.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}