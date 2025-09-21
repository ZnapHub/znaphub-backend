using ZnapHub.Domain.Events;
using ZnapHub.Domain.Interfaces;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Photos;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    FileName FileName,
    DateTimeOffset UploadedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
};
