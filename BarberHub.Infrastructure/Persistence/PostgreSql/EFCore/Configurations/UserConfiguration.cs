using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(m => m.Id);
        
        builder.Property(x=> x. FirstName)
            .HasColumnType(UserConstants.FirstNameColumnType)
            .HasMaxLength(UserConstants.FirstNameMaxLength)
            .IsRequired();
        builder.Property(x => x.LastName)
            .HasColumnType(UserConstants.LastNameColumnType)
            .HasMaxLength(UserConstants.LastNameMaxLength)
            .IsRequired();
        builder.Property(x=> x.MobileNumber)
            .HasColumnType(UserConstants.MobileNumberColumnType)
            .HasMaxLength(UserConstants.MobileNumberMaxLength)
            .IsRequired();
        builder.Property(x=> x.IsMobileVerified)
            .IsRequired();
        builder.Property(x=> x.UserName)
            .HasColumnType(UserConstants.UsernameColumnType)
            .HasMaxLength(UserConstants.UsernameMaxLength)
            .IsRequired();
        builder.Property(x=> x.PasswordHash)
            .HasColumnType(UserConstants.PasswordColumnType)
            .HasMaxLength(UserConstants.PasswordMaxLength)
            .IsRequired();
        
        builder.HasIndex(x => x.UserName).IsUnique();
    }
}