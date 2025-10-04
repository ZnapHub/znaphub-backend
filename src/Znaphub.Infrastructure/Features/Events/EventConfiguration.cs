using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZnapHub.Infrastructure.Data.Features.Events;

internal class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrganizerId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(4000);
        builder.Property(x => x.IsPublic).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.OrganizerId);
    }
}
