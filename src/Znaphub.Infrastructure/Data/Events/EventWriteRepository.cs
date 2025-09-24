using ZnapHub.Domain.Events;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Events;

public sealed class EventWriteRepository : IEventWriteRepository
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
