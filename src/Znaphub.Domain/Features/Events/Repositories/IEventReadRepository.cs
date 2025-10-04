using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.ValueObjects;

namespace ZnapHub.Domain.Features.Events.Repositories;

public interface IEventReadRepository
{
    Task<Event?> GetAsync(EventId id, CancellationToken ct = default);

    Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(
        OrganizerId id,
        CancellationToken ct = default
    );
}
