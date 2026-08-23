using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(x=> x. FirstName)
            .HasColumnType(CustomerConstants.FirstNameColumnType)
            .HasMaxLength(CustomerConstants.FirstNameMaxLength)
            .IsRequired();
        builder.Property(x => x.LastName)
            .HasColumnType(CustomerConstants.LastNameColumnType)
            .HasMaxLength(CustomerConstants.LastNameMaxLength)
            .IsRequired();
        builder.Property(x=> x.MobileNumber)
            .HasColumnType(CustomerConstants.MobileNumberColumnType)
            .HasMaxLength(CustomerConstants.MobileNumberMaxLength)
            .IsRequired();
        builder.Property(x=> x.IsMobileVerified)
            .IsRequired();
        builder.Property(x=> x.UserName)
            .HasColumnType(CustomerConstants.UsernameColumnType)
            .HasMaxLength(CustomerConstants.UsernameMaxLength)
            .IsRequired();
        builder.Property(x=> x.PasswordHash)
            .HasColumnType(CustomerConstants.PasswordColumnType)
            .HasMaxLength(CustomerConstants.PasswordMaxLength)
            .IsRequired();
        
        builder.HasIndex(x => x.UserName).IsUnique();
    }
}