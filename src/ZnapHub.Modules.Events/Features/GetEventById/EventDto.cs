namespace ZnapHub.Modules.Events.Features.GetEventById;

public sealed record EventDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsPublic,
    DateTimeOffset CreatedAt
);
