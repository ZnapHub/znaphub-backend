using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Domain.Events;

public sealed record EventCreated(
    EventId EventId,
    OrganizerId OrganizerId,
    DateTimeOffset CreatedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
