using Microsoft.EntityFrameworkCore;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.Entities;
using ZnapHub.Domain.Features.Photos.Repositories;
using ZnapHub.Domain.Features.Photos.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.Photos.Data;

internal sealed class PhotoReadRepository : IPhotoReadRepository
{
    private readonly ZnapHubReadContext _db;

    public PhotoReadRepository(ZnapHubReadContext db) => _db = db;

    public async Task<Photo?> GetAsync(PhotoId id, CancellationToken ct = default)
    {
        var photo = await _db.Photos.Where(p => p.Id == id).FirstOrDefaultAsync(ct);
        return photo.ToDomain();
    }

    public async Task<IReadOnlyList<Photo>> GetByEventAsync(
        EventId eventId,
        CancellationToken ct = default
    )
    {
        var photos = await _db
            .Photos.Where(p => p.EventId == eventId)
            .OrderByDescending(p => p.UploadedAt)
            .ToListAsync(ct);

        return photos.Select(p => p.ToDomain()).ToList();
    }
}
