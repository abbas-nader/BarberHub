using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.TokenHash)
            .HasMaxLength(RefreshTokenConstants.TokenHashMaxLength)
            .IsRequired();
        builder.Property(x=> x.UserId)
            .IsRequired();
        builder.Property(x=>x.Role)
            .IsRequired();
        builder.Property(x=> x.CreatedAt)
            .IsRequired();
        builder.Property(x=> x.ExpiresAt)
            .IsRequired();
        builder.Property(x=>x.IsRevoked)
            .IsRequired();
        
        builder.HasIndex(x=>x.TokenHash).IsUnique();
        
        builder.HasOne<RefreshToken>()
            .WithOne()
            .HasForeignKey<RefreshToken>(x=>x.ReplacedByTokenId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}