using Znaphub.Domainz.Interfaces;
using Znaphub.Domainz.ValueObjects;

namespace Znaphub.Domainz.Photos;

public sealed record PhotoUploaded(
    PhotoId PhotoId,
    EventId EventId,
    FileName FileName,
    DateTimeOffset UploadedAt
) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
};
