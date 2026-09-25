using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class OtpHistoryConfiguration : IEntityTypeConfiguration<OtpHistory>
{
    public void Configure(EntityTypeBuilder<OtpHistory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Purpose)
            .IsRequired();
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.ExpiresAt)
            .IsRequired();
        builder.Property(x => x.ResolvedAt)
            .IsRequired(false);
        builder.Property(x => x.CodeHash)
            .HasMaxLength(OtpConstants.CodeHashLength)
            .IsRequired();
        builder.Property(x => x.FailedAttempts)
            .IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.UserId, x.Purpose, x.CreatedAt });
    }
}