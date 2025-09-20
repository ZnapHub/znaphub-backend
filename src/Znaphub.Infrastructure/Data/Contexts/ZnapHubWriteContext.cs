using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Infrastructure.Data.Contexts;

public sealed class ZnapHubWriteContext : BaseDbContext
{
    public ZnapHubWriteContext(DbContextOptions<ZnapHubWriteContext> opts)
        : base(opts) { }
}
