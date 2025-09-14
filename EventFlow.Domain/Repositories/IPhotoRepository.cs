using EventFlow.Domain.Entities;
using EventFlow.Domain.ValueObjects;

namespace EventFlow.Domain.Repositories;

public interface IPhotoRepository
{
    Task<Photo?> GetAsync(PhotoId id);
    Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100);
    Task AddAsync(Photo photo);
    Task SaveChangesAsync(CancellationToken ct = default);
}