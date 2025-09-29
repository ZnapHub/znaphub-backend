namespace ZnapHub.Application.Abstractions.Storage;

public interface IUrlService
{
    Task<string> GetUrlAsync(string objectName, TimeSpan? expiry = null);
}
