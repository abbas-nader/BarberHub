using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(a => a.AppointmentStatus).IsRequired();
        builder.Property(a => a.DepositPaymentMethod).IsRequired();
        builder.Property(a => a.DepositStatus).IsRequired();
        builder.ComplexProperty(x => x.DepositAmountSnapshot, money =>
            {
                money.Property(m => m.Value)
                    .HasColumnName("DepositAmountValue")
                    .HasColumnType("numeric(18,2)")
                    .IsRequired();
                money.Property(m => m.Currency)
                    .HasColumnName("DepositAmountCurrency")
                    .IsRequired();
            }
        );
        builder.ComplexProperty(x => x.ServiceSnapshot, serviceSnapShot =>
            {
                serviceSnapShot.Property(s=> s.ServiceName)
                    .HasColumnName("ServiceName")
                    .IsRequired();
                serviceSnapShot.Property(s=> s.ServiceDuration)
                    .HasColumnName("ServiceDuration")
                    .IsRequired();
                serviceSnapShot.ComplexProperty(s => s.ServicePrice, money =>
                {
                    money.Property(m => m.Value)
                        .HasColumnName("ServicePriceValue")
                        .HasColumnType("numeric(18,2)")
                        .IsRequired();
                    money.Property(m => m.Currency)
                        .HasColumnName("ServicePriceCurrency")
                        .IsRequired();
                });
            }
        );
        builder.HasOne<Barber>()
            .WithMany(a=> a.Appointments)
            .HasForeignKey(b => b.BarberId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Customer>()
            .WithMany(a=> a.Appointments)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Salon>()
            .WithMany(a=> a.Appointments)
            .HasForeignKey(b => b.SalonId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<BarberService>()
            .WithMany()
            .HasForeignKey(b => b.BarberServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(w=> w.WalletTransactions)
            .WithOne()
            .HasForeignKey(a => a.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);
             
    }
}