using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.ValueObjects;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Features.Photos.Factories;

public static class ObjectNameFactory
{
    public static ObjectName ForEvent(EventId eventId, PhotoId photoId, string fileName)
    {
        var cleanFileName = Path.GetFileName(fileName.Trim());
        return ObjectName.FromString($"{eventId}/{photoId.Value}-{cleanFileName}");
    }
}
