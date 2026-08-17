using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
{
    public void Configure(EntityTypeBuilder<Gallery> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Caption)
            .HasMaxLength(GalleryConstants.CaptionMaxLength);
        
      builder.HasOne(x=>x.Salon)
          .WithMany(x=> x.Galleries)
          .HasForeignKey(x=>x.SalonId)
          .OnDelete(DeleteBehavior.Restrict);
      builder.HasOne(x=>x.Barber)
          .WithMany(x=>x.Galleries)
          .HasForeignKey(x=>x.BarberId)
          .OnDelete(DeleteBehavior.Restrict);
      builder.HasOne(x=>x.File)
          .WithOne()
          .HasForeignKey<Gallery>(x=>x.FileId)
          .OnDelete(DeleteBehavior.Restrict);
      
    }
}