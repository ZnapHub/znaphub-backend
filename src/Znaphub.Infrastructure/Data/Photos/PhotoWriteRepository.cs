using ZnapHub.Domain.Photos.Entities;
using ZnapHub.Domain.Photos.Interfaces;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Photos;

internal class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public PhotoWriteRepository(ZnapHubWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo) => await _db.Photos.AddAsync(photo.ToEntity());
}
