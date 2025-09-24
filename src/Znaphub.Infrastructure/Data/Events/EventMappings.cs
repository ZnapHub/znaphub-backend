using ZnapHub.Domain.Events.Entities;
using ZnapHub.Domain.Events.ValueObjects;

namespace ZnapHub.Infrastructure.Data.Events;

internal static class EventMappings
{
    public static Event ToDomain(this EventEntity entity)
    {
        EventTimeRange timeRange = entity.EndsAt.HasValue
            ? new EventTimeRange.Fixed(
                EventDate.FromDateTimeOffset(entity.StartsAt),
                EventDate.FromDateTimeOffset(entity.EndsAt.Value)
            )
            : new EventTimeRange.OpenEnded(EventDate.FromDateTimeOffset(entity.StartsAt));

        var domain = Event.Rehydrate(
            EventId.FromGuid(entity.Id),
            OrganizerId.FromGuid(entity.OrganizerId),
            EventName.FromString(entity.Name),
            EventSlug.FromString(entity.Slug),
            EventVisibility.FromBool(entity.IsPublic),
            timeRange,
            entity.CreatedAt,
            entity.Description is null ? null : EventDescription.FromString(entity.Description),
            entity.UpdatedAt
        );

        return domain;
    }

    public static EventEntity ToEntity(this Event domain) =>
        new()
        {
            Id = domain.Id.Value,
            OrganizerId = domain.OrganizerId.Value,
            Name = domain.Name.Value,
            Slug = domain.Slug.Value,
            Description = domain.Description.Value,
            StartsAt = domain.TimeRange.Start,
            EndsAt = domain.TimeRange.End,
            IsPublic = domain.Visibility is EventVisibility.Public,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
        };
}
