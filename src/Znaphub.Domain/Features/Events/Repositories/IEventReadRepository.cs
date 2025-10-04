using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.ValueObjects;

namespace ZnapHub.Domain.Features.Events.Interfaces;

public interface IEventReadRepository
{
    Task<Event?> GetAsync(EventId id);

    Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(OrganizerId id);
}
