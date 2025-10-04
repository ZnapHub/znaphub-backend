using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Infrastructure.Data.Features.QrCodes;

internal static class QrCodeMapper
{
    internal static QrCode ToDomain(this QrCodeEntity? entity)
    {
        if (entity is null)
            return null!;

        return QrCode.Rehydrate(
            QrCodeId.FromGuid(entity.Id),
            ShortId.FromString(entity.ShortId),
            EventId.FromGuid(entity.EventId),
            MapToState(entity),
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    internal static QrCodeEntity ToEntity(this QrCode domain)
    {
        var entity = new QrCodeEntity
        {
            Id = domain.Id,
            ShortId = domain.ShortId,
            EventId = domain.EventId,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
        };

        switch (domain.State)
        {
            case QrCodeState.Active active:
                entity.IsActive = true;
                entity.ExpiresAt = active.ExpiresAt;
                entity.MaxUploads = active.MaxUploads;
                entity.UploadCount = active.UploadCount;
                break;

            case QrCodeState.Expired expired:
                entity.IsActive = false;
                entity.ExpiresAt = expired.ExpiredAt;
                break;

            case QrCodeState.Deactivated:
                entity.IsActive = false;
                break;
        }

        return entity;
    }

    private static QrCodeState MapToState(QrCodeEntity entity)
    {
        if (entity.ExpiresAt.HasValue && entity.ExpiresAt.Value <= DateTimeOffset.UtcNow)
        {
            return new QrCodeState.Expired(entity.ExpiresAt.Value);
        }

        if (!entity.IsActive)
        {
            return entity.ExpiresAt.HasValue
                ? new QrCodeState.Expired(entity.ExpiresAt.Value)
                : new QrCodeState.Deactivated();
        }

        return new QrCodeState.Active(entity.ExpiresAt, entity.MaxUploads, entity.UploadCount);
    }
}
