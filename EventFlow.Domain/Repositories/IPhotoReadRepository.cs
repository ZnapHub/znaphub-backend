using EventFlow.Domain.Entities;
using EventFlow.Domain.ValueObjects;
using EventFlow.Domain.ValueObjects.Photos;

namespace EventFlow.Domain.Repositories;

public interface IPhotoReadRepository
{
    Task<Photo?> GetAsync(PhotoId id);
    Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100);
}
