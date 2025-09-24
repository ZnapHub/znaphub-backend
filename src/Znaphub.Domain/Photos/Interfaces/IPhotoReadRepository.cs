using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Photos.Entities;
using ZnapHub.Domain.Photos.ValueObjects;

namespace ZnapHub.Domain.Photos.Interfaces;

public interface IPhotoReadRepository
{
    Task<Photo?> GetAsync(PhotoId id);
    Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100);
}
