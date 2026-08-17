using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class BarberServiceConfiguration : IEntityTypeConfiguration<BarberService>
{
    public void Configure(EntityTypeBuilder<BarberService> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ComplexProperty(x => x.Money, money =>
            {
                money.Property(m => m.Value)
                    .HasColumnName(BarberServiceConstants.PriceValueColumnName)
                    .HasColumnType(BarberServiceConstants.PriceValueColumnType)
                    .IsRequired();
                money.Property(m => m.Currency)
                    .HasColumnName(BarberServiceConstants.PriceCurrencyColumnName)
                    .IsRequired();
            }
        );
        builder.HasOne<Service>()
            .WithMany()
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Barber>()
            .WithMany()
            .HasForeignKey(x => x.BarberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}