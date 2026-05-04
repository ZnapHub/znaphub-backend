using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.QrCodes.Domain.Events;

public sealed record QrCodeGenerated(QrCodeId QrCodeId, Guid EventId, QrCodeState State)
    : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
