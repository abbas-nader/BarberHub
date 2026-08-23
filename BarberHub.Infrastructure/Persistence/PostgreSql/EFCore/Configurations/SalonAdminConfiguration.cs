using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class SalonAdminConfiguration  :IEntityTypeConfiguration<SalonAdmin>
{
    public void Configure(EntityTypeBuilder<SalonAdmin> builder)
    {
        builder.HasKey(sa => sa.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(SalonAdminConstants.FirstNameMaxLength)
            .IsRequired();
        builder.Property(x => x.LastName)
            .HasMaxLength(SalonAdminConstants.LastNameMaxLength)
            .IsRequired();
        builder.Property(x=> x.UserName)
            .HasMaxLength(SalonAdminConstants.UsernameMaxLength)
            .IsRequired();
        builder.Property(x=> x.PasswordHash)
            .HasMaxLength(SalonAdminConstants.PasswordMaxLength)
            .IsRequired();
        builder.Property(x=> x.MobileNumber)
            .HasMaxLength(SalonAdminConstants.PhoneNumberMaxLength)
            .IsRequired();
        
        builder.HasOne<Salon>()
            .WithOne(x=> x.SalonAdmin)
            .HasForeignKey<SalonAdmin>(x=>x.SalonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserName).IsUnique();
    }
}