using ZnapHub.Domain.Events;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Photos;
using ZnapHub.Domain.Photos.Entities;
using ZnapHub.Domain.Photos.ValueObjects;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Infrastructure.Data.Photos;

internal static class PhotoMappings
{
    public static Photo ToDomain(this PhotoEntity? e)
    {
        if (e is null)
            return null!;

        return Photo.Rehydrate(
            PhotoId.FromGuid(e.Id),
            EventId.FromGuid(e.EventId),
            FileName.FromString(e.FileName),
            ObjectName.FromString(e.ObjectName),
            PhotoUrl.FromString(e.Url),
            e.UploadedAt
        );
    }

    public static PhotoEntity ToEntity(this Photo domain) =>
        new()
        {
            Id = domain.Id.Value,
            EventId = domain.EventId.Value,
            FileName = domain.FileName,
            ObjectName = domain.ObjectName,
            Url = domain.Url,
            UploadedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
        };
}
