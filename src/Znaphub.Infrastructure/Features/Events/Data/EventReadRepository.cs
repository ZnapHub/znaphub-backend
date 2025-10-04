using Microsoft.EntityFrameworkCore;
using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.Repositories;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.Events.Data;

internal sealed class EventReadRepository : IEventReadRepository
{
    private readonly ZnapHubReadContext _db;

    public EventReadRepository(ZnapHubReadContext db)
    {
        _db = db;
    }

    public async Task<Event?> GetAsync(EventId id, CancellationToken ct = default)
    {
        var @event = await _db.Events.Where(e => e.Id == id).FirstOrDefaultAsync(ct);
        return @event.ToDomain();
    }

    public async Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(
        OrganizerId id,
        CancellationToken ct = default
    )
    {
        var events = await _db.Events.Where(e => e.OrganizerId == id).ToListAsync(ct);
        return events.Select(e => e.ToDomain()).ToList();
    }
}
