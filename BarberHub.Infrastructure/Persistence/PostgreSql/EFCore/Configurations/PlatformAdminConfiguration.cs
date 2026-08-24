using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class PlatformAdminConfiguration : IEntityTypeConfiguration<PlatformAdmin>
{
    public void Configure(EntityTypeBuilder<PlatformAdmin> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x=> x.FirstName)
            .IsRequired()
            .HasMaxLength(PlatformAdminConstants.FirstNameMaxLength);
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(PlatformAdminConstants.LastNameMaxLength);
        builder.Property(x=> x.UserName)
            .IsRequired()
            .HasMaxLength(PlatformAdminConstants.UsernameMaxLength);
        builder.Property(x=> x.PasswordHash)
            .IsRequired()
            .HasMaxLength(PlatformAdminConstants.PasswordMaxLength);
        
        builder.HasIndex(x => x.UserName).IsUnique();
    }
}