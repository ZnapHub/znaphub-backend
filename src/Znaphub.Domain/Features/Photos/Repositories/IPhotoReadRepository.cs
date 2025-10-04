using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.Entities;
using ZnapHub.Domain.Features.Photos.ValueObjects;

namespace ZnapHub.Domain.Features.Photos.Repositories;

public interface IPhotoReadRepository
{
    Task<Photo?> GetAsync(PhotoId id, CancellationToken ct = default);
    Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, CancellationToken ct = default);
}
