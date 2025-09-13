using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Persistence.Contexts;


public sealed class ReadDbContext : BaseDbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> opts) : base(opts)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}