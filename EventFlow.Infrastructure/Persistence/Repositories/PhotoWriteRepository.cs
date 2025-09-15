using EventFlow.Domain.Entities;
using EventFlow.Domain.Repositories;
using EventFlow.Infrastructure.Mappings;
using EventFlow.Infrastructure.Persistence.Contexts;

namespace EventFlow.Infrastructure.Persistence.Repositories;

public class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly EventFlowWriteContext _db;

    public PhotoWriteRepository(EventFlowWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo) => await _db.Photos.AddAsync(photo.ToEntity());
}
