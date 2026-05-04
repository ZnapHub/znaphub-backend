using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.Repositories;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;
using ZnapHub.Shared.Contracts.Identity;

namespace ZnapHub.Modules.Events.Features.CreateEvent;

public sealed class CreateEventHandler
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEventRepository _events;

    public CreateEventHandler(ICurrentUserService currentUser, IEventRepository events)
    {
        _currentUser = currentUser;
        _events = events;
    }

    public async Task<Result> HandleAsync(
        CreateEventCommand command,
        CancellationToken ct = default
    )
    {
        var userId = _currentUser.UserId;
        if (userId is null)
            return Error.NullValue;

        var @event = Event.Create(
            OrganizerId.FromGuid(userId.Value),
            EventName.FromString(command.Name),
            EventSlug.FromString(command.Slug),
            EventVisibility.FromBool(command.IsPublic),
            EventDescription.FromString(command.Description)
        );

        await _events.AddAsync(@event, ct);

        return Result.Success();
    }
}
