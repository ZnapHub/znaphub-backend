namespace ZnapHub.Application.Features.Events.Dtos;

public sealed record EventDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    bool IsPublic,
    DateTimeOffset CreatedAt
);
