using ZnapHub.Modules.QrCodes.Domain.Errors;
using ZnapHub.Modules.QrCodes.Domain.Events;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.QrCodes.Domain.Entities;

public sealed class QrCode : Entity<QrCodeId>, IAggregateRoot
{
    public ShortId ShortId { get; }
    public Guid EventId { get; }
    public QrCodeState State { get; private set; }

    private QrCode(
        QrCodeId id,
        ShortId shortId,
        Guid eventId,
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

    public static QrCode Create(
        ShortId shortId,
        Guid eventId,
        int maxUploads,
        DateTimeOffset? expiresAt = null
    )
    {
        var id = QrCodeId.New();
        var state = new QrCodeState.Active(expiresAt, maxUploads, UploadCount: 0);
        var qrCode = new QrCode(id, shortId, eventId, state, DateTimeOffset.UtcNow);
        qrCode.Raise(new QrCodeGenerated(id, eventId, state));
        return qrCode;
    }

    public static QrCode Rehydrate(
        QrCodeId id,
        ShortId shortId,
        Guid eventId,
        QrCodeState state,
        DateTimeOffset createdAt,
        DateTimeOffset? updatedAt = null
    ) => new(id, shortId, eventId, state, createdAt, updatedAt);

    public Result IncrementUploadCount()
    {
        if (State is not QrCodeState.Active active)
            return QrCodeErrors.CannotIncrementInactive;

        var newCount = active.UploadCount + 1;

        if (active.MaxUploads > 0 && newCount >= active.MaxUploads)
        {
            State = new QrCodeState.Expired(DateTimeOffset.UtcNow, active.MaxUploads, newCount);
            Raise(new QrCodeExpired(Id, EventId));
        }
        else
        {
            State = active with { UploadCount = newCount };
        }

        UpdatedAt = DateTimeOffset.UtcNow;
        return Result.Success();
    }

    public void Deactivate()
    {
        if (State is not QrCodeState.Active active)
            return;
        State = new QrCodeState.Deactivated(active.MaxUploads, active.UploadCount);
        UpdatedAt = DateTimeOffset.UtcNow;
        Raise(new QrCodeDeactivated(Id, EventId));
    }

    public bool CanUpload() =>
        State switch
        {
            QrCodeState.Active active => (
                !active.ExpiresAt.HasValue || active.ExpiresAt > DateTimeOffset.UtcNow
            ) && (active.MaxUploads == 0 || active.UploadCount < active.MaxUploads),
            _ => false,
        };
}
