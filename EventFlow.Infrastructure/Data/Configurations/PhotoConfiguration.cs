using EventFlow.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Data.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<PhotoEntity>
{
    public void Configure(EntityTypeBuilder<PhotoEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EventId).IsRequired().HasMaxLength(128);
        builder.Property(x => x.FileName).IsRequired().HasMaxLength(512);
        builder.Property(x => x.ObjectName).IsRequired().HasMaxLength(1024);
        builder.Property(x => x.Url).IsRequired().HasMaxLength(2048);
        builder.Property(x => x.UploadedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.HasIndex(x => new { x.EventId, x.UploadedAt });
    }
}
