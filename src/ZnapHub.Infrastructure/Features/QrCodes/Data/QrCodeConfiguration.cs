using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZnapHub.Domain.Features.Events.Entities;

namespace ZnapHub.Infrastructure.Features.QrCodes.Data;

internal class QrCodeConfiguration : IEntityTypeConfiguration<QrCodeEntity>
{
    public void Configure(EntityTypeBuilder<QrCodeEntity> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedNever();
        builder.Property(q => q.ShortId).IsRequired().HasMaxLength(32);
        builder.HasIndex(q => q.ShortId).IsUnique();
        builder.Property(q => q.EventId).IsRequired();
        builder.Property(q => q.CreatedAt).IsRequired();
        builder.Property(q => q.UpdatedAt).IsRequired(false);
        builder.Property(q => q.ExpiresAt).IsRequired(false);
        builder.Property(q => q.MaxUploads).IsRequired();
        builder.Property(q => q.UploadCount).IsRequired();
        builder.Property(q => q.IsActive).IsRequired();

        builder
            .HasOne<Event>()
            .WithMany()
            .HasForeignKey(q => q.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
