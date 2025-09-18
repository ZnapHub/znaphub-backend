using EventFlow.Domain.Interfaces;
using EventFlow.Domain.ValueObjects;
using EventFlow.Domain.ValueObjects.Photos;

namespace EventFlow.Domain.Entities;

public sealed class Photo : IEntity<PhotoId>
{
    public PhotoId Id { get; }
    public EventId EventId { get; }
    public FileName FileName { get; }
    public ObjectName ObjectName { get; }
    public PhotoUrl Url { get; private set; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }

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
    ) => new(PhotoId.New(), eventId, fileName, objectName, url, DateTimeOffset.UtcNow);

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

    public Photo UpdateUrl(string newUrl)
    {
        Url = PhotoUrl.FromString(newUrl);
        UpdatedAt = DateTimeOffset.UtcNow;
        return this;
    }
}
