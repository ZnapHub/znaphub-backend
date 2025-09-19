using EventFlow.Domain.Interfaces;
using EventFlow.Domain.ValueObjects;

namespace EventFlow.Domain.Photos;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    FileName FileName,
    DateTimeOffset UploadedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
};
