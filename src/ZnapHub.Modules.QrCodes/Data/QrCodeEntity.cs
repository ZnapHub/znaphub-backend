namespace ZnapHub.Modules.QrCodes.Data;

internal sealed class QrCodeEntity
{
    public Guid Id { get; set; }
    public string ShortId { get; set; } = null!;
    public Guid EventId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public int MaxUploads { get; set; }
    public int UploadCount { get; set; }
    public bool IsActive { get; set; } = true;
}
