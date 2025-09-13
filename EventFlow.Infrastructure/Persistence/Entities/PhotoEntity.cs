namespace EventFlow.Infrastructure.Persistence.Entities;

public sealed class PhotoEntity
{
    public Guid Id { get; set; }
    public string EventId { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ObjectName { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTimeOffset UploadedAt { get; set; }
}