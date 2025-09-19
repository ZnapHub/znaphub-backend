using EventFlow.Infrastructure.Data.Configurations;
using EventFlow.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Data.Contexts;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PhotoConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
