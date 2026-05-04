using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Modules.Events.Data;

public sealed class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options)
        : base(options) { }

    internal DbSet<EventEntity> Events => Set<EventEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("events");
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
