using ZnapHub.Domain.Events;
using ZnapHub.Domain.Events.Entities;
using ZnapHub.Domain.Events.Interfaces;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Events;

public sealed class EventReadRepository : IEventReadRepository
{
    private readonly ZnapHubReadContext _db;

    public EventReadRepository(ZnapHubReadContext db)
    {
        _db = db;
    }

    public Task<Event?> GetAsync(EventId id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(OrganizerId id)
    {
        throw new NotImplementedException();
    }
}
