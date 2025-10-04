using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.ValueObjects;

namespace ZnapHub.Infrastructure.Data.Features.Events;

internal static class EventMapper
{
    internal static Event ToDomain(this EventEntity? entity)
    {
        if (entity is null)
            return null!;

        var domain = Event.Rehydrate(
            EventId.FromGuid(entity.Id),
            OrganizerId.FromGuid(entity.OrganizerId),
            EventName.FromString(entity.Name),
            EventSlug.FromString(entity.Slug),
            EventVisibility.FromBool(entity.IsPublic),
            entity.CreatedAt,
            entity.Description is null ? null : EventDescription.FromString(entity.Description),
            entity.UpdatedAt
        );

        return domain;
    }

    internal static EventEntity ToEntity(this Event domain) =>
        new()
        {
            Id = domain.Id.Value,
            OrganizerId = domain.OrganizerId.Value,
            Name = domain.Name.Value,
            Slug = domain.Slug.Value,
            Description = domain.Description.Value,
            IsPublic = domain.Visibility is EventVisibility.Public,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
        };
}
