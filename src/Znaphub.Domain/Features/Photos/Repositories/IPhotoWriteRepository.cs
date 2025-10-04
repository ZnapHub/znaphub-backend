using ZnapHub.Domain.Features.Photos.Entities;

namespace ZnapHub.Domain.Features.Photos.Repositories;

public interface IPhotoWriteRepository
{
    Task AddAsync(Photo photo);
}
