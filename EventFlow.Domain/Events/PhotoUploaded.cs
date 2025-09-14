using EventFlow.Domain.ValueObjects;

namespace EventFlow.Domain.Events;

public sealed record PhotoUploaded(PhotoId PhotoId, EventId EventId, string FileName, DateTimeOffset UploadedAt);