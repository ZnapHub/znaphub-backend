using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Domain.Repositories;

public interface IEventRepository
{
    Task AddAsync(Event @event, CancellationToken ct = default);

    Task<Event?> GetByIdAsync(EventId id, CancellationToken ct = default);

    Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(
        OrganizerId id,
        CancellationToken ct = default
    );
}
