using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Identity;
using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Dtos;
using ZnapHub.Application.Features.Events.Mappers;
using ZnapHub.Domain.Features.Events.Interfaces;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Features.Events.Queries.GetEventsByOrganizer;

public sealed class GetEventsByOrganizerQueryHandler
    : IQueryHandler<GetEventsByOrganizerQuery, IReadOnlyList<EventDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventReadRepository _eventReadRepository;

    public GetEventsByOrganizerQueryHandler(
        ICurrentUserService currentUserService,
        IEventReadRepository eventReadRepository
    ) => (_currentUserService, _eventReadRepository) = (currentUserService, eventReadRepository);

    public async Task<Result<IReadOnlyList<EventDto>>> HandleAsync(GetEventsByOrganizerQuery query)
    {
        var organizerId = _currentUserService.UserId;
        if (organizerId is null)
            return Result.Failure<IReadOnlyList<EventDto>>(Error.NullValue);

        var events = await _eventReadRepository.GetByOrganizerIdAsync(
            OrganizerId.FromGuid(organizerId.Value)
        );

        return events.Select(e => e.ToDto()).ToList();
    }
}
