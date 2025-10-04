using Microsoft.EntityFrameworkCore;
using ZnapHub.Infrastructure.Features.Events.Data;
using ZnapHub.Infrastructure.Features.Photos;
using ZnapHub.Infrastructure.Features.Photos.Data;
using ZnapHub.Infrastructure.Features.QrCodes.Data;

namespace ZnapHub.Infrastructure.Data.Contexts;

internal abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();

    public DbSet<EventEntity> Events => Set<EventEntity>();

    public DbSet<QrCodeEntity> QrCodes => Set<QrCodeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
