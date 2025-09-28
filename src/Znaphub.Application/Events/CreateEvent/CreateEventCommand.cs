using ZnapHub.Application.Abstractions.Messaging.Commands;

namespace ZnapHub.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
    string EventName,
    string EventSlug,
    bool IsPublic,
    string? Description = null
) : ICommand;
