using Znaphub.Domainz.ValueObjects;

namespace Znaphub.Domainz.Photos;

public interface IPhotoReadRepository
{
    Task<Photo?> GetAsync(PhotoId id);
    Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100);
}
