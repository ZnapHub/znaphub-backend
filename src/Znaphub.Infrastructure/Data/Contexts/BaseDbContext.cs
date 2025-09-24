using Microsoft.EntityFrameworkCore;
using ZnapHub.Infrastructure.Data.Events;
using ZnapHub.Infrastructure.Data.Photos;

namespace ZnapHub.Infrastructure.Data.Contexts;

internal abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();

    public DbSet<EventEntity> Events => Set<EventEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
