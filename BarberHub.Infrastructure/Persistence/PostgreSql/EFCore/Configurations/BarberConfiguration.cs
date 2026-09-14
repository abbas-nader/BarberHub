using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class BarberConfiguration : IEntityTypeConfiguration<Barber>
{
    public void Configure(EntityTypeBuilder<Barber> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(x => x.Description).HasMaxLength(BarberConstants.DescriptionMaxLength);
        builder.Property(x => x.IsActive).IsRequired();

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<Barber>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.UserId).IsUnique();

        builder.HasOne<Salon>()
            .WithMany(b => b.Barbers)
            .HasForeignKey(m => m.SalonId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.WorkSchedules)
            .WithOne()
            .HasForeignKey(w => w.BarberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}