using Minio;
using Minio.DataModel.Args;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Storage.Providers;

public sealed class MinioUrlService : IUrlService
{
    private readonly IMinioClient _client;

    public MinioUrlService(IMinioClient client) => _client = client;

    public async Task<string> GetUrlAsync(string objectName, TimeSpan? expiry = null)
    {
        var expiryTimespan = expiry ?? TimeSpan.FromMinutes(7);

        var args = new PresignedGetObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectName)
            .WithExpiry((int)expiryTimespan.TotalSeconds);

        return await _client.PresignedGetObjectAsync(args);
    }
}
