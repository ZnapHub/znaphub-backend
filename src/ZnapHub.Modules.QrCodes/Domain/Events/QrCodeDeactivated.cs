using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.QrCodes.Domain.Events;

public sealed record QrCodeDeactivated(QrCodeId QrCodeId, Guid EventId) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
