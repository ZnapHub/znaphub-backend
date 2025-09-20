using Znaphub.Domain.Photos;
using ZnapHub.Infrastructurez.Data.Contexts;
using ZnapHub.Infrastructurez.Data.Mappings;

namespace ZnapHub.Infrastructurez.Data.Repositories;

public class PhotoWriteRepository : IPhotoWriteRepository
{
    private readonly EventFlowWriteContext _db;

    public PhotoWriteRepository(EventFlowWriteContext db) => _db = db;

    public async Task AddAsync(Photo photo) => await _db.Photos.AddAsync(photo.ToEntity());
}
