using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.Entities;
using ZnapHub.Domain.Features.Photos.ValueObjects;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Infrastructure.Features.Photos.Data;

internal static class PhotoMapper
{
    internal static Photo ToDomain(this PhotoEntity? e)
    {
        if (e is null)
            return null!;

        return Photo.Rehydrate(
            PhotoId.FromGuid(e.Id),
            EventId.FromGuid(e.EventId),
            FileName.FromString(e.FileName),
            ObjectName.FromString(e.ObjectName),
            e.UploadedAt
        );
    }

    internal static PhotoEntity ToEntity(this Photo domain) =>
        new()
        {
            Id = domain.Id.Value,
            EventId = domain.EventId,
            FileName = domain.FileName,
            ObjectName = domain.ObjectName,
            UploadedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
        };
}
