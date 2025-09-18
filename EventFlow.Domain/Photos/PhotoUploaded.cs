using EventFlow.Domain.ValueObjects;

namespace EventFlow.Domain.Photos;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    FileName FileName,
    DateTimeOffset UploadedAt
);
