namespace ZnapHub.Infrastructure.Storage.Interfaces;

internal interface IUrlProvider
{
    Task<string> GetUrlAsync(string bucket, string objectName, TimeSpan? expiry = null);
}
