using EventFlow.Infrastructure.Persistence.Contexts;
using EventFlow.Infrastructure.Persistence.Entities;

namespace EventFlow.Infrastructure.Persistence.Repositories;

public class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly WriteDbContext _db;
    public PhotoWriteRepository(WriteDbContext db) => _db = db;

    public async Task AddAsync(PhotoEntity entity) => await _db.Photos.AddAsync(entity);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await _db.SaveChangesAsync(ct);
}