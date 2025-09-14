using EventFlow.Infrastructure.Persistence.Entities;

namespace EventFlow.Infrastructure.Persistence.Repositories;

public interface IPhotoReadRepository
{
    Task<IReadOnlyList<PhotoEntity>> GetByEventAsync(string eventId, int limit = 100);
    Task<PhotoEntity?> GetByIdAsync(Guid id);
}