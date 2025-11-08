using ZnapHub.Application.Abstractions.Messaging.Commands;

namespace ZnapHub.Application.Features.Events.Commands.CreateEvent;

public sealed record CreateEventCommand(
    string Name,
    string Slug,
    bool IsPublic,
    string? Description = null
) : ICommand;
