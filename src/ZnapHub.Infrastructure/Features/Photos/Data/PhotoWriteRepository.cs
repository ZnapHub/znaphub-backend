using ZnapHub.Domain.Features.Photos.Entities;
using ZnapHub.Domain.Features.Photos.Repositories;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.Photos.Data;

internal class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public PhotoWriteRepository(ZnapHubWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo, CancellationToken ct = default) =>
        await _db.Photos.AddAsync(photo.ToEntity(), ct);
}
