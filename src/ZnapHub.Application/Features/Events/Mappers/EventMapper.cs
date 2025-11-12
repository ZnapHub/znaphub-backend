using ZnapHub.Application.Features.Events.Dtos;
using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.ValueObjects;

namespace ZnapHub.Application.Features.Events.Mappers;

internal static class EventMapper
{
    extension(Event @event)
    {
        internal EventDto ToDto() =>
            new(
                @event.Id,
                @event.Name,
                @event.Slug,
                @event.Description,
                @event.Visibility is EventVisibility.Public,
                @event.CreatedAt
            );
    }
}
