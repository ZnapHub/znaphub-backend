using ZnapHub.Domain.Abstractions;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;
using ZnapHub.Domain.Interfaces;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Features.QrCodes.Entities;

public sealed class QrCode : Entity<QrCodeId>, IAggregateRoot
{
    public ShortId ShortId { get; }

    public EventId EventId { get; }

    public QrCodeState State { get; private set; }

    private QrCode(
        QrCodeId id,
        ShortId shortId,
        EventId eventId,
        QrCodeState state,
        DateTimeOffset createdAt,
        DateTimeOffset? updatedAt
    )
    {
        Id = id;
        ShortId = shortId;
        EventId = eventId;
        State = state;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
