using ZnapHub.Domain.Photos;
using ZnapHub.Domain.ValueObjects;
using ZnapHub.Infrastructure.Data.Entities;

namespace ZnapHub.Infrastructure.Data.Mappings;

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
