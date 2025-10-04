using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;

namespace ZnapHub.Infrastructure.Features.QrCodes;

internal static class QrCodeMapper
{
    internal static QrCode ToDomain(this QrCodeEntity? entity)
    {
        if (entity is null)
            return null!;

        return QrCode.Rehydrate(
            entity.Id,
            entity.ShortId,
            EventId.FromGuid(entity.EventId),
            entity.ToState(),
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
                entity.MaxUploads = expired.MaxUploads;
                entity.UploadCount = expired.UploadCount;
                break;

            case QrCodeState.Deactivated deactivated:
                entity.IsActive = false;
                entity.ExpiresAt = null;
                entity.MaxUploads = deactivated.MaxUploads;
                entity.UploadCount = deactivated.UploadCount;
                break;
        }

        return entity;
    }

    private static QrCodeState ToState(this QrCodeEntity entity)
    {
        if (entity.ExpiresAt.HasValue && entity.ExpiresAt.Value <= DateTimeOffset.UtcNow)
        {
            return new QrCodeState.Expired(
                entity.ExpiresAt.Value,
                entity.MaxUploads,
                entity.UploadCount
            );
        }

        if (!entity.IsActive)
        {
            return entity.ExpiresAt.HasValue
                ? new QrCodeState.Expired(
                    entity.ExpiresAt.Value,
                    entity.MaxUploads,
                    entity.UploadCount
                )
                : new QrCodeState.Deactivated(entity.MaxUploads, entity.UploadCount);
        }

        return new QrCodeState.Active(entity.ExpiresAt, entity.MaxUploads, entity.UploadCount);
    }
}
