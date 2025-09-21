using ZnapHub.Domain.Abstractions;
using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Events;

public sealed class Event : Entity<EventId>, IAggregateRoot
{
    public OrganizerId OrganizerId { get; }

    private Event(EventId eventId, OrganizerId organizerId)
    {
        Id = eventId;
        OrganizerId = organizerId;
    }
}