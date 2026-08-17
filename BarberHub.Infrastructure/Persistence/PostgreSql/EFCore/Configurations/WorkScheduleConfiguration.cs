using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class WorkScheduleConfiguration  :IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.StartTime)
            .IsRequired();
        builder.Property(x=>x.EndTime)
            .IsRequired();
        builder.Property(x=>x.DayOfWeek)
            .IsRequired();
    }
}