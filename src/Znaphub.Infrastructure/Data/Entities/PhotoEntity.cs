namespace ZnapHub.Infrastructure.Data.Entities;

public sealed class PhotoEntity
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string FileName { get; set; } = null!;
    public string ObjectName { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTimeOffset UploadedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
