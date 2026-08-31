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
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(BarberConstants.FirstNameMaxLength);
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(BarberConstants.LastNameMaxLength);
        builder.Property(x => x.MobileNumber)
            .IsRequired()
            .HasMaxLength(BarberConstants.MobileMaxLength);
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(BarberConstants.UserNameMaxLength);
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(BarberConstants.PasswordHashMaxLength);
        builder.Property(x => x.Description)
            .HasMaxLength(BarberConstants.DescriptionMaxLength);
        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.HasOne<Salon>()
            .WithMany(b => b.Barbers)
            .HasForeignKey(m => m.SalonId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.WorkSchedules)
            .WithOne()
            .HasForeignKey(w => w.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.UserName).IsUnique();
    }
}