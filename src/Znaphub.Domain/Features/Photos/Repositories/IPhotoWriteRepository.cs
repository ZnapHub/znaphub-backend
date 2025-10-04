using ZnapHub.Domain.Features.Photos.Entities;

namespace ZnapHub.Domain.Features.Photos.Interfaces;

public interface IPhotoWriteRepository
{
    Task AddAsync(Photo photo);
}
