using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Data;

internal static class QrCodeDataMapper
{
    extension(QrCodeEntity entity)
    {
        internal QrCode ToDomain()
        {
            QrCodeState state = entity switch
            {
                { IsActive: true } => new QrCodeState.Active(
                    entity.ExpiresAt,
                    entity.MaxUploads,
                    entity.UploadCount
                ),
                { IsActive: false, ExpiresAt: { } expiredAt } => new QrCodeState.Expired(
                    expiredAt,
                    entity.MaxUploads,
                    entity.UploadCount
                ),
                _ => new QrCodeState.Deactivated(entity.MaxUploads, entity.UploadCount),
            };

            return QrCode.Rehydrate(
                QrCodeId.FromGuid(entity.Id),
                ShortId.FromString(entity.ShortId),
                entity.EventId,
                state,
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }

    extension(QrCode domain)
    {
        internal QrCodeEntity ToEntity()
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
    }
}
