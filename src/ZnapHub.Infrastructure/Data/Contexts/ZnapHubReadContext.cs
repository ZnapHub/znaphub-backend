using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Infrastructure.Data.Contexts;

internal sealed class ZnapHubReadContext : BaseDbContext
{
    public ZnapHubReadContext(DbContextOptions<ZnapHubReadContext> opts)
        : base(opts)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}
