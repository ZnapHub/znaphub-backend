using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZnapHub.Modules.QrCodes.Data;

internal sealed class QrCodeConfiguration : IEntityTypeConfiguration<QrCodeEntity>
{
    public void Configure(EntityTypeBuilder<QrCodeEntity> builder)
    {
        builder.ToTable("qr_codes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ShortId).IsRequired().HasMaxLength(32);
        builder.HasIndex(x => x.ShortId).IsUnique();
        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.ExpiresAt).IsRequired(false);
        builder.Property(x => x.MaxUploads).IsRequired();
        builder.Property(x => x.UploadCount).IsRequired();
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasIndex(x => x.EventId);
    }
}
