using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnapHub.Infrastructure.Data.Entities;

namespace ZnapHub.Infrastructure.Data.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<PhotoEntity>
{
    public void Configure(EntityTypeBuilder<PhotoEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.FileName).IsRequired().HasMaxLength(512);
        builder.Property(x => x.ObjectName).IsRequired().HasMaxLength(1024);
        builder.Property(x => x.Url).IsRequired().HasMaxLength(2048);
        builder.Property(x => x.UploadedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.HasIndex(x => new { x.EventId, x.UploadedAt });
    }
}
