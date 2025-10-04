using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.Repositories;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.Events.Data;

internal sealed class EventWriteRepository : IEventWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public EventWriteRepository(ZnapHubWriteContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Event @event, CancellationToken ct = default) =>
        await _db.AddAsync(@event.ToEntity(), ct);
}
