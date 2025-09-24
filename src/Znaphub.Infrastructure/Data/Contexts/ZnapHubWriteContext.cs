using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Infrastructure.Data.Contexts;

internal sealed class ZnapHubWriteContext : BaseDbContext
{
    public ZnapHubWriteContext(DbContextOptions<ZnapHubWriteContext> opts)
        : base(opts) { }
}
