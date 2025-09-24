using ZnapHub.Application.Abstractions.Messaging.Commands;

namespace ZnapHub.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
    string EventName,
    bool IsPublic,
    DateTimeOffset StartsAt,
    DateTimeOffset? EndsAt,
    string? Description = null
) : ICommand;
