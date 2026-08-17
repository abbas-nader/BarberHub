using BarberHub.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.HasKey(x=> x.Id);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(FileConstants.FileNameMaxLength);
        builder.Property(x=> x.Url)
            .IsRequired()
            .HasMaxLength(FileConstants.UrlMaxLength);
        builder.Property(x=> x.OriginFileName)
            .IsRequired()
            .HasMaxLength(FileConstants.OriginFileNameMaxLength);
        builder.Property(x=> x.ContentType)
            .IsRequired()
            .HasMaxLength(FileConstants.ContentTypeMaxLength);
        builder.Property(x => x.Size)
            .IsRequired();

    }
}