namespace Znaphub.Domainz.Photos;

public interface IPhotoWriteRepository
{
    Task AddAsync(Photo photo);
}
