namespace EventFlow.Domain.Photos;

public interface IPhotoWriteRepository
{
    Task AddAsync(Photo photo);
}
