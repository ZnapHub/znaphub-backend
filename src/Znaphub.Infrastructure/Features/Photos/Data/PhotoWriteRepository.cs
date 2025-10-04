using ZnapHub.Domain.Features.Photos.Entities;
using ZnapHub.Domain.Features.Photos.Repositories;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.Photos;

internal class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public PhotoWriteRepository(ZnapHubWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo) => await _db.Photos.AddAsync(photo.ToEntity());
}
