using ZnapHub.Domain.Events.Entities;
using ZnapHub.Domain.Events.Interfaces;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Events;

internal sealed class EventWriteRepository : IEventWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public EventWriteRepository(ZnapHubWriteContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Event @event)
    {
        throw new NotImplementedException();
    }
}
