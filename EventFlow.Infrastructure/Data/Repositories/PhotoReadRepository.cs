using EventFlow.Domain.Entities;
using EventFlow.Domain.Repositories;
using EventFlow.Domain.ValueObjects;
using EventFlow.Domain.ValueObjects.Photos;
using EventFlow.Infrastructure.Data.Contexts;
using EventFlow.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Data.Repositories;

public class PhotoReadRepository : IPhotoReadRepository
{
    private readonly EventFlowReadContext _db;

    public PhotoReadRepository(EventFlowReadContext db) => _db = db;

    public async Task<Photo?> GetAsync(PhotoId id) =>
        await _db.Photos.Select(p => p.ToDomain()).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IReadOnlyList<Photo>> GetByEventAsync(EventId eventId, int limit = 100) =>
        await _db
            .Photos.Where(p => p.EventId == eventId.ToString())
            .OrderByDescending(p => p.UploadedAt)
            .Take(limit)
            .Select(p => p.ToDomain())
            .ToListAsync();
}
