using ZnapHub.Infrastructure.Data.Features.Events;

namespace ZnapHub.Infrastructure.Data.Features.Photos;

internal sealed class PhotoEntity
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public EventEntity Event { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ObjectName { get; set; } = null!;
    public DateTimeOffset UploadedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
