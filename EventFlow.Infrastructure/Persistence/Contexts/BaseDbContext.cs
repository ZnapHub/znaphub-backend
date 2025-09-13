using EventFlow.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Persistence.Contexts;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options) { }

    public DbSet<PhotoEntity> Photos => Set<PhotoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhotoEntity>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.EventId).IsRequired().HasMaxLength(128);
            b.Property(x => x.FileName).IsRequired().HasMaxLength(512);
            b.Property(x => x.ObjectName).IsRequired().HasMaxLength(1024);
            b.Property(x => x.Url).IsRequired().HasMaxLength(2048);
            b.Property(x => x.UploadedAt).IsRequired();
            b.HasIndex(x => new { x.EventId, x.UploadedAt });
        });

        base.OnModelCreating(modelBuilder);
    }
}