namespace ZnapHub.Domain.Events;

public interface IEventReadRepository
{
    Task<Event?> GetAsync(EventId id);

    Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(OrganizerId id);
}
