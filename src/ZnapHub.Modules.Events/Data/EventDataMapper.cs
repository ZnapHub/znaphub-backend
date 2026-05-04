using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Data;

internal static class EventDataMapper
{
    extension(EventEntity entity)
    {
        internal Event ToDomain() =>
            Event.Rehydrate(
                EventId.FromGuid(entity.Id),
                OrganizerId.FromGuid(entity.OrganizerId),
                EventName.FromString(entity.Name),
                EventSlug.FromString(entity.Slug),
                EventVisibility.FromBool(entity.IsPublic),
                entity.CreatedAt,
                entity.Description is null ? null : EventDescription.FromString(entity.Description),
                entity.UpdatedAt
            );
    }

    extension(Event domain)
    {
        internal EventEntity ToEntity() =>
            new()
            {
                Id = domain.Id,
                OrganizerId = domain.OrganizerId,
                Name = domain.Name.Value,
                Slug = domain.Slug.ToString(),
                Description = domain.Description.ToString(),
                IsPublic = domain.Visibility is EventVisibility.Public,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
            };
    }
}
