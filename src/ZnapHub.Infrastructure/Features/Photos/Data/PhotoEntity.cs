using ZnapHub.Infrastructure.Features.Events.Data;

namespace ZnapHub.Infrastructure.Features.Photos.Data;

internal sealed class PhotoEntity
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string FileName { get; set; } = null!;
    public string ObjectName { get; set; } = null!;
    public DateTimeOffset UploadedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public EventEntity Event { get; set; } = null!;
}
