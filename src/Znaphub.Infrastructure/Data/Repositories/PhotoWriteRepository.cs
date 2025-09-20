using ZnapHub.Domain.Photos;
using ZnapHub.Infrastructure.Data.Contexts;
using ZnapHub.Infrastructure.Data.Mappings;

namespace ZnapHub.Infrastructure.Data.Repositories;

public class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public PhotoWriteRepository(ZnapHubWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo) => await _db.Photos.AddAsync(photo.ToEntity());
}
