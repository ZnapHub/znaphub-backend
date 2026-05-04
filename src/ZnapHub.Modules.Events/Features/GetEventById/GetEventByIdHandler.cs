using ZnapHub.Modules.Events.Domain.Repositories;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Features.GetEventById;

public sealed class GetEventByIdHandler
{
    private readonly IEventRepository _events;

    public GetEventByIdHandler(IEventRepository events) => _events = events;

    public async Task<Result<EventDto>> HandleAsync(
        GetEventByIdQuery query,
        CancellationToken ct = default
    )
    {
        var @event = await _events.GetByIdAsync(EventId.FromGuid(query.EventId), ct);
        return @event is null ? Error.NotFound : @event.ToDto();
    }
}
