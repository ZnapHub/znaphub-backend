using ZnapHub.Domain.Events;
using ZnapHub.Domain.ValueObjects;

namespace ZnapHub.Domain.Photos;

public interface IPhotoReadRepository
{
    Task<Photo?> GetAsync(PhotoId id);
    Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100);
}
