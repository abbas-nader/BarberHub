using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class SalonAdminConfiguration : IEntityTypeConfiguration<SalonAdmin>
{
    public void Configure(EntityTypeBuilder<SalonAdmin> builder)
    {   builder.HasKey(sa => sa.Id);

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<SalonAdmin>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.UserId).IsUnique();

        builder.HasOne(x => x.Salon)
            .WithMany()
            .HasForeignKey(x => x.SalonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}