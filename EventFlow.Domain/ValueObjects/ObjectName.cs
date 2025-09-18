namespace EventFlow.Domain.ValueObjects;

public sealed record ObjectName(string Value)
{
    public static ObjectName ForEvent(EventId eventId, PhotoId photoId, string fileName)
    {
        var cleanFileName = Path.GetFileName(fileName.Trim());
        return new ObjectName($"{eventId.Value}/{photoId.Value}-{cleanFileName}");
    }

    public override string ToString() => Value;

    public static implicit operator string(ObjectName objectName) => objectName.ToString();
}
