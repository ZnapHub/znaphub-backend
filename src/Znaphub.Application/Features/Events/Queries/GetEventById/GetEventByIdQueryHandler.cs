using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Dtos;
using ZnapHub.Application.Features.Events.Mappers;
using ZnapHub.Domain.Features.Events.Repositories;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Features.Events.Queries.GetEventById;

public sealed class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, EventDto>
{
    private readonly IEventReadRepository _eventReadRepository;

    public GetEventByIdQueryHandler(IEventReadRepository eventReadRepository) =>
        _eventReadRepository = eventReadRepository;

    public async Task<Result<EventDto>> HandleAsync(GetEventByIdQuery query)
    {
        var @event = await _eventReadRepository.GetAsync(EventId.FromGuid(query.EventId));
        if (@event is null)
            return Error.NotFound;

        return @event.ToDto();
    }
}
