using Microsoft.EntityFrameworkCore;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Photos.Entities;
using ZnapHub.Domain.Photos.Interfaces;
using ZnapHub.Domain.Photos.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Photos;

internal sealed class PhotoReadRepository : IPhotoReadRepository
{
    private readonly ZnapHubReadContext _db;

    public PhotoReadRepository(ZnapHubReadContext db) => _db = db;

    public async Task<Photo?> GetAsync(PhotoId id)
    {
        var photo = await _db.Photos.FindAsync(id);
        return photo.ToDomain();
    }

    public async Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100)
    {
        var photos = await _db
            .Photos.Where(p => p.EventId == eventId.Value)
            .OrderByDescending(p => p.UploadedAt)
            .Take(limit)
            .ToListAsync();

        return photos.Select(p => p.ToDomain()).ToList();
    }
}
