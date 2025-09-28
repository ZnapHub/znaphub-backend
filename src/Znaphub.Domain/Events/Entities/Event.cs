using ZnapHub.Domain.Abstractions;
using ZnapHub.Domain.Events.Events;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Events.Entities;

public sealed class Event : Entity<EventId>, IAggregateRoot
{
    public OrganizerId OrganizerId { get; }

    public EventName Name { get; private set; }

    public EventSlug Slug { get; }

    public EventDescription Description { get; private set; }

    public EventVisibility Visibility { get; private set; }

    private Event(
        EventId eventId,
        OrganizerId organizerId,
        EventName name,
        EventSlug slug,
        EventDescription description,
        EventVisibility visibility,
        DateTimeOffset createdAt,
        DateTimeOffset? updatedAt = null
    )
    {
        Id = eventId;
        OrganizerId = organizerId;
        Name = name;
        Slug = slug;
        Description = description;
        Visibility = visibility;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Event Create(
        OrganizerId organizerId,
        EventName name,
        EventSlug slug,
        EventVisibility visibility,
        EventDescription? description = null
    )
    {
        var @event = new Event(
            EventId.New(),
            organizerId,
            name,
            slug,
            description ?? EventDescription.Empty,
            visibility,
            DateTimeOffset.UtcNow
        );
        @event.Raise(new EventCreated(@event.Id, @event.OrganizerId, @event.CreatedAt));
        return @event;
    }

    public static Event Rehydrate(
        EventId eventId,
        OrganizerId organizerId,
        EventName name,
        EventSlug slug,
        EventVisibility visibility,
        DateTimeOffset createdAt,
        EventDescription? description = null,
        DateTimeOffset? updatedAt = null
    ) =>
        new(
            eventId,
            organizerId,
            name,
            slug,
            description ?? EventDescription.Empty,
            visibility,
            createdAt,
            updatedAt
        );

    public Event UpdateVisibility(EventVisibility visibility)
    {
        Visibility = visibility;
        UpdatedAt = DateTimeOffset.UtcNow;
        return this;
    }

    public Event UpdateDetails(EventName name, EventDescription description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTimeOffset.UtcNow;
        return this;
    }
}
