using EventFlow.Domain.Entities;
using EventFlow.Domain.Repositories;
using EventFlow.Infrastructure.Data.Contexts;
using EventFlow.Infrastructure.Data.Mappings;

namespace EventFlow.Infrastructure.Data.Repositories;

public class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly EventFlowWriteContext _db;

    public PhotoWriteRepository(EventFlowWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo) => await _db.Photos.AddAsync(photo.ToEntity());
}
