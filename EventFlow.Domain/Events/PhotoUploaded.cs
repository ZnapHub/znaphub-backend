using EventFlow.Domain.ValueObjects;
using EventFlow.Domain.ValueObjects.Photos;

namespace EventFlow.Domain.Events;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    string FileName,
    DateTimeOffset UploadedAt
);
