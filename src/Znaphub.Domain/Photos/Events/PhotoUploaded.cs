using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Interfaces;
using ZnapHub.Domain.Photos.ValueObjects;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Photos.Events;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    FileName FileName,
    DateTimeOffset UploadedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
};
