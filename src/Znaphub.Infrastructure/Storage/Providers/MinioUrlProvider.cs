using Minio;
using Minio.DataModel.Args;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Storage.Providers;

internal sealed class MinioUrlProvider : IUrlProvider
{
    private readonly IMinioClient _client;

    public MinioUrlProvider(IMinioClient client) => _client = client;

    public async Task<string> GetUrlAsync(string bucket, string objectName, TimeSpan? expiry = null)
    {
        var expiryTimespan = expiry ?? TimeSpan.FromMinutes(7);

        var args = new PresignedGetObjectArgs()
            .WithBucket(bucket)
            .WithObject(objectName)
            .WithExpiry((int)expiryTimespan.TotalSeconds);

        return await _client.PresignedGetObjectAsync(args);
    }
}
