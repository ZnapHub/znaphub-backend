using ZnapHub.Domain.Abstractions;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.Events;
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
        DateTimeOffset? updatedAt = null
    )
    {
        Id = id;
        ShortId = shortId;
        EventId = eventId;
        State = state;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static QrCode Create(QrCodeId id, ShortId shortId, EventId eventId, QrCodeState state)
    {
        var qrCode = new QrCode(id, shortId, eventId, state, DateTimeOffset.UtcNow);
        qrCode.Raise(new QrCodeGenerated(id, eventId, state));
        return qrCode;
    }

    public static QrCode Rehydrate(
        QrCodeId id,
        ShortId shortId,
        EventId eventId,
        QrCodeState state,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt
    ) => new(id, shortId, eventId, state, createdAt, updatedAt);
}
