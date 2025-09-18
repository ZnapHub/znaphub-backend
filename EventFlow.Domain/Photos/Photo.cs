using EventFlow.Domain.Abstractions;
using EventFlow.Domain.ValueObjects;

namespace EventFlow.Domain.Photos;

public sealed class Photo : Entity<PhotoId>
{
    public EventId EventId { get; }
    public FileName FileName { get; }
    public ObjectName ObjectName { get; }
    public PhotoUrl Url { get; }

    private Photo(
        PhotoId id,
        EventId eventId,
        FileName fileName,
        ObjectName objectName,
        PhotoUrl url,
        DateTimeOffset uploadedAt,
        DateTimeOffset? updatedAt = null
    )
    {
        Id = id;
        EventId = eventId;
        FileName = fileName;
        ObjectName = objectName;
        Url = url;
        CreatedAt = uploadedAt;
        UpdatedAt = updatedAt;
    }

    public static Photo Create(
        EventId eventId,
        FileName fileName,
        ObjectName objectName,
        PhotoUrl url
    )
    {
        Photo photo = new(PhotoId.New(), eventId, fileName, objectName, url, DateTimeOffset.UtcNow);
        photo.Raise(new PhotoUploaded(photo.Id, photo.EventId, photo.FileName, photo.CreatedAt));
        return photo;
    }

    public static Photo Rehydrate(
        PhotoId id,
        EventId eventId,
        FileName fileName,
        ObjectName objectName,
        PhotoUrl url,
        DateTimeOffset uploadedAt,
        DateTimeOffset? updatedAt = null
    )
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(eventId);
        return new Photo(id, eventId, fileName, objectName, url, uploadedAt, updatedAt);
    }
}
