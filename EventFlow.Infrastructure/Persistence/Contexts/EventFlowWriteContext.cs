using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Persistence.Contexts;

public sealed class WriteDbContext : BaseDbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> opts) : base(opts) { }
}