using ZnapHub.Domain.Events.Entities;
using ZnapHub.Domain.Events.ValueObjects;

namespace ZnapHub.Domain.Events.Interfaces;

public interface IEventReadRepository
{
    Task<Event?> GetAsync(EventId id);

    Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(OrganizerId id);
}
