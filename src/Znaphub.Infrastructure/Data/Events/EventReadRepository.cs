using Microsoft.EntityFrameworkCore;
using ZnapHub.Domain.Events.Entities;
using ZnapHub.Domain.Events.Interfaces;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Events;

internal sealed class EventReadRepository : IEventReadRepository
{
    private readonly ZnapHubReadContext _db;

    public EventReadRepository(ZnapHubReadContext db)
    {
        _db = db;
    }

    public async Task<Event?> GetAsync(EventId id)
    {
        var @event = await _db.Events.FindAsync(id);
        return @event.ToDomain();
    }

    public async Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(OrganizerId id)
    {
        var events = await _db.Events.Where(e => e.OrganizerId == id).ToListAsync();
        return events.Select(e => e.ToDomain()).ToList();
    }
}
