using ZnapHub.Domain.Photos.Entities;

namespace ZnapHub.Domain.Photos.Interfaces;

public interface IPhotoWriteRepository
{
    Task AddAsync(Photo photo);
}
