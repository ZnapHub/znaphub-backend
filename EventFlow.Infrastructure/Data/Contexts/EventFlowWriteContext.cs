using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Data.Contexts;

public sealed class EventFlowWriteContext : BaseDbContext
{
    public EventFlowWriteContext(DbContextOptions<EventFlowWriteContext> opts)
        : base(opts) { }
}
