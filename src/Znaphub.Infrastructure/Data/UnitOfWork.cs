using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ZnapHubWriteContext _db;

    public UnitOfWork(ZnapHubWriteContext db) => _db = db;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _db.SaveChangesAsync(cancellationToken);
}
