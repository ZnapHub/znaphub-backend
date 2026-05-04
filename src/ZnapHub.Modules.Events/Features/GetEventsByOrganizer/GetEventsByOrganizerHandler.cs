using ZnapHub.Modules.Events.Domain.Repositories;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Modules.Events.Features.GetEventById;
using ZnapHub.Shared.Abstractions;
using ZnapHub.Shared.Contracts.Identity;

namespace ZnapHub.Modules.Events.Features.GetEventsByOrganizer;

public sealed class GetEventsByOrganizerHandler
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEventRepository _events;

    public GetEventsByOrganizerHandler(ICurrentUserService currentUser, IEventRepository events)
    {
        _currentUser = currentUser;
        _events = events;
    }

    public async Task<Result<IReadOnlyList<EventDto>>> HandleAsync(
        GetEventsByOrganizerQuery query,
        CancellationToken ct = default
    )
    {
        var organizerId = _currentUser.UserId;
        if (organizerId is null)
            return Result.Failure<IReadOnlyList<EventDto>>(Error.NullValue);

        var events = await _events.GetByOrganizerIdAsync(
            OrganizerId.FromGuid(organizerId.Value),
            ct
        );

        return Result.Success<IReadOnlyList<EventDto>>([.. events.Select(e => e.ToDto())]);
    }
}
