using Microsoft.EntityFrameworkCore;
using Znaphub.Domain.Photos;
using Znaphub.Domain.ValueObjects;
using ZnapHub.Infrastructurez.Data.Contexts;
using ZnapHub.Infrastructurez.Data.Mappings;

namespace ZnapHub.Infrastructurez.Data.Repositories;

public class PhotoReadRepository : IPhotoReadRepository
{
    private readonly EventFlowReadContext _db;

    public PhotoReadRepository(EventFlowReadContext db) => _db = db;

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
