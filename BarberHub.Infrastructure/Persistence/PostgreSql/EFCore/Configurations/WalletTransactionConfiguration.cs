using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(x => x.Amount, money =>
        {
            money.Property(x=>x.Value)
                .HasColumnName(WalletTransactionConstants.AmountValueColumnName)
                .HasColumnType(WalletTransactionConstants.AmountValueColumnTypeName)
                .IsRequired();
            money.Property(x => x.Currency)
                .HasColumnName(WalletTransactionConstants.AmountCurrencyColumnName)
                .IsRequired();
        });
        builder.ComplexProperty(x => x.BalanceAfterTransaction, money =>
        {
            money.Property(x=>x.Value)
                .HasColumnName(WalletTransactionConstants.BalanceAfterTransactionValueColumnName)
                .HasColumnType(WalletTransactionConstants.BalanceAfterTransactionValueColumnTypeName)
                .IsRequired();
            money.Property(x => x.Currency)
                .HasColumnName(WalletTransactionConstants.BalanceAfterTransactionCurrencyColumnName)
                .IsRequired();
        });
        builder.HasOne<Customer>()
            .WithMany(x => x.WalletTransactions)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Appointment>()
            .WithMany(x => x.WalletTransactions)
            .HasForeignKey(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}