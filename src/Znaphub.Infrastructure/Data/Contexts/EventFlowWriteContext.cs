using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Infrastructurez.Data.Contexts;

public sealed class EventFlowWriteContext : BaseDbContext
{
    public EventFlowWriteContext(DbContextOptions<EventFlowWriteContext> opts)
        : base(opts) { }
}
