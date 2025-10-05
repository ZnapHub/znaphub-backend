using Amazon.S3;
using Amazon.S3.Model;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Storage.Providers;

internal sealed class S3UrlProvider : IUrlProvider
{
    private readonly IAmazonS3 _s3Client;

    public S3UrlProvider(IAmazonS3 s3Client) => _s3Client = s3Client;

    public async Task<string> GetUrlAsync(string bucket, string objectName, TimeSpan? expiry = null)
    {
        var expiryTimespan = expiry ?? TimeSpan.FromMinutes(60);

        var args = new GetPreSignedUrlRequest
        {
            BucketName = bucket,
            Key = objectName,
            Expires = DateTime.UtcNow.Add(expiryTimespan),
            Verb = HttpVerb.GET,
        };

        return await _s3Client.GetPreSignedURLAsync(args);
    }
}
