using ZnapHub.Domain.Events;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Photos;
using ZnapHub.Domain.Photos.ValueObjects;

namespace ZnapHub.Domain.ValueObjects;

public sealed record ObjectName
{
    public string Value { get; }

    private ObjectName(string value) => Value = value;

    public static ObjectName ForEvent(EventId eventId, PhotoId photoId, string fileName)
    {
        var cleanFileName = Path.GetFileName(fileName.Trim());
        return new ObjectName($"{eventId.Value}/{photoId.Value}-{cleanFileName}");
    }

    public static ObjectName FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(ObjectName objectName) => objectName.ToString();
}
