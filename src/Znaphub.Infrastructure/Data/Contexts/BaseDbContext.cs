using Microsoft.EntityFrameworkCore;
using ZnapHub.Infrastructure.Data.Configurations;
using ZnapHub.Infrastructure.Data.Entities;

namespace ZnapHub.Infrastructure.Data.Contexts;

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
