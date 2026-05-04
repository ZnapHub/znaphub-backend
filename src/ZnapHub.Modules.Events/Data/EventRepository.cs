using Microsoft.EntityFrameworkCore;
using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.Repositories;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Data;

internal sealed class EventRepository : IEventRepository
{
    private readonly EventsDbContext _db;

    public EventRepository(EventsDbContext db) => _db = db;

    public async Task AddAsync(Event @event, CancellationToken ct = default)
    {
        await _db.Events.AddAsync(@event.ToEntity(), ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<Event?> GetByIdAsync(EventId id, CancellationToken ct = default)
    {
        var entity = await _db.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == (Guid)id, ct);
        return entity?.ToDomain();
    }

    public async Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(
        OrganizerId id,
        CancellationToken ct = default
    )
    {
        var entities = await _db
            .Events.AsNoTracking()
            .Where(e => e.OrganizerId == (Guid)id)
            .ToListAsync(ct);
        return [.. entities.Select(e => e.ToDomain())];
    }
}
