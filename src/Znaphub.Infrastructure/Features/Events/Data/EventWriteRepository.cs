using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.Repositories;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.Events;

internal sealed class EventWriteRepository : IEventWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public EventWriteRepository(ZnapHubWriteContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Event @event) => await _db.AddAsync(@event.ToEntity());
}
