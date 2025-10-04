using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;
using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Features.QrCodes.Events;

public sealed record QrCodeDeactivated(QrCodeId Id, EventId EventId) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
