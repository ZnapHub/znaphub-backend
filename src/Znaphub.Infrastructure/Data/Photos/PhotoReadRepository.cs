using Microsoft.EntityFrameworkCore;
using ZnapHub.Domain.Events;
using ZnapHub.Domain.Photos;
using ZnapHub.Domain.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Photos;

public class PhotoReadRepository : IPhotoReadRepository
{
    private readonly ZnapHubReadContext _db;

    public PhotoReadRepository(ZnapHubReadContext db) => _db = db;

    public async Task<Photo?> GetAsync(PhotoId id) =>
        await _db.Photos.Select(p => p.ToDomain()).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100) =>
        await _db
            .Photos.Where(p => p.EventId == eventId.Value)
            .OrderByDescending(p => p.UploadedAt)
            .Take(limit)
            .Select(p => p.ToDomain())
            .ToListAsync();
}
