using EventFlow.Infrastructure.Persistence.Contexts;
using EventFlow.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Persistence.Repositories;

public class PhotoReadRepository : IPhotoReadRepository
{
    private readonly ReadDbContext _db;
    public PhotoReadRepository(ReadDbContext db) => _db = db;

    public async Task<IReadOnlyList<PhotoEntity>> GetByEventAsync(string eventId, int limit = 100) =>
        await _db.Photos
            .Where(p => p.EventId == eventId)
            .OrderByDescending(p => p.UploadedAt)
            .Take(limit)
            .ToListAsync();

    public async Task<PhotoEntity?> GetByIdAsync(Guid id)
        => await _db.Photos.FindAsync(id);
}