using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(ServiceConstants.NameMaxLength)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(ServiceConstants.DescriptionMaxLength);
        builder.Property(x => x.Duration)
            .IsRequired();
        
        builder.HasOne<Salon>()
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.SalonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}