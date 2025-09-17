using EventFlow.Domain.ValueObjects;

namespace EventFlow.Domain.Entities;

public sealed class Photo
{
    public PhotoId Id { get; }
    public EventId EventId { get; }
    public string FileName { get; }
    public string ObjectName { get; }
    public string Url { get; private set; }
    public DateTimeOffset UploadedAt { get; }

    private Photo(
        PhotoId id,
        EventId eventId,
        string fileName,
        string objectName,
        string url,
        DateTimeOffset uploadedAt
    )
    {
        Id = id;
        EventId = eventId;
        FileName = fileName;
        ObjectName = objectName;
        Url = url;
        UploadedAt = uploadedAt;
    }

    public static Photo Create(EventId eventId, string fileName, string objectName, string url) =>
        new Photo(PhotoId.New(), eventId, fileName, objectName, url, DateTimeOffset.UtcNow);

    public static Photo Rehydrate(
        PhotoId id,
        EventId eventId,
        string fileName,
        string objectName,
        string url,
        DateTimeOffset uploadedAt
    )
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(eventId);
        return new Photo(id, eventId, fileName.Trim(), objectName.Trim(), url.Trim(), uploadedAt);
    }

    public void UpdateUrl(string newUrl)
    {
        if (string.IsNullOrWhiteSpace(newUrl))
            throw new ArgumentException("Url cannot be empty");
        Url = newUrl;
    }
}
