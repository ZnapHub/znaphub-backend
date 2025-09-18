using EventFlow.Domain.Entities;
using EventFlow.Domain.ValueObjects;
using EventFlow.Infrastructure.Data.Entities;

namespace EventFlow.Infrastructure.Mappings;

internal static class PhotoMappings
{
    public static Photo ToDomain(this PhotoEntity? e)
    {
        if (e is null)
            return null!;
        return Photo.Rehydrate(
            new PhotoId(e.Id),
            new EventId(e.EventId),
            e.FileName,
            e.ObjectName,
            e.Url,
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
            UploadedAt = domain.UploadedAt,
        };
}
