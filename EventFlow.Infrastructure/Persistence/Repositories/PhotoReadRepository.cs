using EventFlow.Domain.Entities;
using EventFlow.Domain.Repositories;
using EventFlow.Domain.ValueObjects;
using EventFlow.Infrastructure.Mappings;
using EventFlow.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Persistence.Repositories;

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
