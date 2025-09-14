using EventFlow.Infrastructure.Persistence.Entities;

namespace EventFlow.Infrastructure.Persistence.Repositories;

public interface IPhotoWriteRepository
{
    Task AddAsync(PhotoEntity entity);
    Task SaveChangesAsync(CancellationToken ct = default);
}
