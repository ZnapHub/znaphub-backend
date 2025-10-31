using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.ValueObjects;
using ZnapHub.Domain.Interfaces;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Features.Photos.Events;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    FileName FileName,
    DateTimeOffset UploadedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
};
