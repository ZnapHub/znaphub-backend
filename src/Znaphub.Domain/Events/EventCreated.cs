using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Events;

public sealed record EventCreated(
    EventId EventId,
    OrganizerId OrganizerId,
    DateTimeOffset CreatedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
