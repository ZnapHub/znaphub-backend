using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;
using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Features.QrCodes.Events;

public record QrCodeGenerated(QrCodeId Id, EventId EventId, QrCodeState State) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
