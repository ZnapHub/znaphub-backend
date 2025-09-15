using EventFlow.Domain.Entities;

namespace EventFlow.Domain.Repositories;

public interface IPhotoWriteRepository
{
    Task AddAsync(Photo photo);
}
