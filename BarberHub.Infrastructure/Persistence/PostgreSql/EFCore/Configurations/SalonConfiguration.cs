using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder.HasKey(salon => salon.Id);
        
        builder.Property(x=>x.Name)
            .HasMaxLength(SalonConstants.NameMaxLength)
            .IsRequired();
        builder.Property(x=>x.Address)
            .HasMaxLength(SalonConstants.AddressMaxLength)
            .IsRequired();
        builder.Property(x=>x.City)
            .HasMaxLength(SalonConstants.CityMaxLength)
            .IsRequired();
        builder.Property(x=>x.PhoneNumber)
            .HasMaxLength(SalonConstants.PhoneNumberMaxLength)
            .IsRequired();
        builder.ComplexProperty(x => x.DepositAmount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName(SalonConstants.DepositAmountValueColumnName)
                .HasColumnType(SalonConstants.DepositAmountColumnType)
                .IsRequired();
            money.Property(m => m.Currency)
                .HasColumnName(SalonConstants.DepositAmountCurrencyCodeColumnName)
                .IsRequired();
        });
        builder.Property(x => x.Description)
            .HasMaxLength(SalonConstants.DescriptionMaxLength);
        builder.Property(x=> x.IsActive)
            .IsRequired();
        
        builder.HasMany(x=>x.Services)
            .WithOne()
            .HasForeignKey(x=>x.SalonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}