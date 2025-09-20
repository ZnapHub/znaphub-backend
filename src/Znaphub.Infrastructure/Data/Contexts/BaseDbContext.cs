using Microsoft.EntityFrameworkCore;
using ZnapHub.Infrastructurez.Data.Configurations;
using ZnapHub.Infrastructurez.Data.Entities;

namespace ZnapHub.Infrastructurez.Data.Contexts;

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
