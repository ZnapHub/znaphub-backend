using EventFlow.Application.Abstractions.Data;
using EventFlow.Infrastructure.Data.Contexts;

namespace EventFlow.Infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly EventFlowWriteContext _db;

    public UnitOfWork(EventFlowWriteContext db) => _db = db;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _db.SaveChangesAsync(cancellationToken);
}
