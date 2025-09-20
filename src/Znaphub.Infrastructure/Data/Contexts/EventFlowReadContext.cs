using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Infrastructurez.Data.Contexts;

public sealed class EventFlowReadContext : BaseDbContext
{
    public EventFlowReadContext(DbContextOptions<EventFlowReadContext> opts)
        : base(opts)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}
