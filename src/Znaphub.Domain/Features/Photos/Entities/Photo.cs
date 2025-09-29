using ZnapHub.Domain.Abstractions;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.Events;
using ZnapHub.Domain.Features.Photos.ValueObjects;
using ZnapHub.Domain.Interfaces;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Features.Photos.Entities;

public sealed class Photo : Entity<PhotoId>, IAggregateRoot
{
    public EventId EventId { get; }
    public FileName FileName { get; }
    public ObjectName ObjectName { get; }

    private Photo(
        PhotoId id,
        EventId eventId,
        FileName fileName,
        ObjectName objectName,
        DateTimeOffset uploadedAt,
        DateTimeOffset? updatedAt = null
    )
    {
        Id = id;
        EventId = eventId;
        FileName = fileName;
        ObjectName = objectName;
        CreatedAt = uploadedAt;
        UpdatedAt = updatedAt;
    }

    public static Photo Create(EventId eventId, FileName fileName, ObjectName objectName)
    {
        Photo photo = new(PhotoId.New(), eventId, fileName, objectName, DateTimeOffset.UtcNow);
        photo.Raise(new PhotoUploaded(photo.Id, photo.EventId, photo.FileName, photo.CreatedAt));
        return photo;
    }

    public static Photo Rehydrate(
        PhotoId id,
        EventId eventId,
        FileName fileName,
        ObjectName objectName,
        DateTimeOffset uploadedAt,
        DateTimeOffset? updatedAt = null
    )
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(eventId);
        return new Photo(id, eventId, fileName, objectName, uploadedAt, updatedAt);
    }
}
