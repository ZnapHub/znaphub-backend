namespace ZnapHub.Modules.Events.Features.CreateEvent;

public sealed record CreateEventCommand(
    string Name,
    string Slug,
    bool IsPublic = false,
    string? Description = null
);
