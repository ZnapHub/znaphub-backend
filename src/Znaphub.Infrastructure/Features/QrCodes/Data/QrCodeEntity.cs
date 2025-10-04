using ZnapHub.Infrastructure.Features.Events.Data;

namespace ZnapHub.Infrastructure.Features.QrCodes;

internal sealed class QrCodeEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ShortId { get; set; } = null!;
    public Guid EventId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public int MaxUploads { get; set; }
    public int UploadCount { get; set; }
    public bool IsActive { get; set; } = true;

    public EventEntity Event { get; set; } = null!;
}
