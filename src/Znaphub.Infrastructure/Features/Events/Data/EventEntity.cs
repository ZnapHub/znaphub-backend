using ZnapHub.Infrastructure.Features.Photos.Data;
using ZnapHub.Infrastructure.Features.QrCodes.Data;

namespace ZnapHub.Infrastructure.Features.Events.Data;

internal sealed class EventEntity
{
    public Guid Id { get; set; }
    public Guid OrganizerId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<PhotoEntity> Photos { get; set; } = [];
    public ICollection<QrCodeEntity> QrCodes { get; set; } = [];
}
