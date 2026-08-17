using BarberHub.Domain.Constants;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rating)
            .IsRequired();
        builder.Property(x => x.Comment)
            .HasMaxLength(ReviewConstants.CommentMaxLength)
            .IsRequired();
        builder.Property(x=>x.IsApproved)
            .IsRequired();
        builder.Property(x => x.Reply)
            .HasMaxLength(ReviewConstants.ReplyMaxLength);
        
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Barber>()
            .WithMany()
            .HasForeignKey(x => x.BarberId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Appointment>()
            .WithOne()
            .HasForeignKey<Review>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Salon>()
            .WithMany()
            .HasForeignKey(x => x.SalonId)
            .OnDelete(DeleteBehavior.Restrict);
        

    }
}