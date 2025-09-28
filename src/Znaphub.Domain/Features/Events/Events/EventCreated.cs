using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Features.Events.Events;

public sealed record EventCreated(
    EventId EventId,
    OrganizerId OrganizerId,
    DateTimeOffset CreatedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
