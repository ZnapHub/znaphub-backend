namespace ZnapHub.Infrastructure.Storage.Interfaces;

public interface IUrlService
{
    Task<string> GetUrlAsync(string bucket, string objectName, TimeSpan? expiry = null);
}
