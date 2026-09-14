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
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(UserConstants.FirstNameMaxLength)
                .IsRequired();
            builder.Property(x => x.LastName)
                .HasMaxLength(UserConstants.LastNameMaxLength)
                .IsRequired();
            builder.Property(x => x.UserName)
                .HasMaxLength(UserConstants.UsernameMaxLength)
                .IsRequired();
            builder.Property(x => x.PasswordHash)
                .HasMaxLength(UserConstants.PasswordMaxLength)
                .IsRequired();
            builder.Property(x => x.Role)
                .IsRequired();
            builder.Property(x => x.MobileNumber)
                .HasMaxLength(UserConstants.MobileNumberMaxLength);

            builder.HasIndex(x => x.UserName).IsUnique();
        }
}