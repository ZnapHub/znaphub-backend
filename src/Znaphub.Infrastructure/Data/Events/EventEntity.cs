using ZnapHub.Infrastructure.Data.Photos;

namespace ZnapHub.Infrastructure.Data.Events;

internal sealed class EventEntity
{
    public Guid Id { get; set; }
    public Guid OrganizerId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
    public bool IsPublic { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<PhotoEntity> Photos { get; set; } = [];
}
