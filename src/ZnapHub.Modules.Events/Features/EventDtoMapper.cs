using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Modules.Events.Features.GetEventById;

namespace ZnapHub.Modules.Events.Features;

internal static class EventDtoMapper
{
    extension(Event @event)
    {
        internal EventDto ToDto() =>
            new(
                @event.Id,
                @event.Name,
                @event.Slug.ToString(),
                @event.Description.ToString(),
                @event.Visibility is EventVisibility.Public,
                @event.CreatedAt
            );
    }
}
