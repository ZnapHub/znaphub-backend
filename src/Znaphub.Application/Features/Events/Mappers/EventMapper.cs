using ZnapHub.Application.Features.Events.Dtos;
using ZnapHub.Domain.Features.Events.Entities;
using ZnapHub.Domain.Features.Events.ValueObjects;

namespace ZnapHub.Application.Features.Events.Mappers;

public static class EventMapper
{
    public static EventDto ToDto(this Event @event) =>
        new(
            @event.Id,
            @event.Name,
            @event.Slug,
            @event.Description,
            @event.Visibility is EventVisibility.Public,
            @event.CreatedAt
        );
}
