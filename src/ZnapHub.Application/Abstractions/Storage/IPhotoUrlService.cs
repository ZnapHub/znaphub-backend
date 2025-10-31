namespace ZnapHub.Application.Abstractions.Storage;

public interface IPhotoUrlService
{
    Task<string> GetUrlAsync(string objectName, TimeSpan? expiry = null);
}
