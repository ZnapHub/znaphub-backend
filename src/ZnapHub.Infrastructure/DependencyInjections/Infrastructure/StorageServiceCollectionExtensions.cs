using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Infrastructure.Features.Photos.Services;
using ZnapHub.Infrastructure.Storage.Interfaces;
using ZnapHub.Infrastructure.Storage.Providers;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class StorageServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddStorage(IConfiguration configuration) =>
            services.AddS3Storage(configuration).AddStorageServices(configuration);

        private IServiceCollection AddS3Storage(IConfiguration configuration)
        {
            services.AddSingleton<IAmazonS3>(_ => new AmazonS3Client(
                configuration["Storage:Cloudflare:AccessKey"],
                configuration["Storage:Cloudflare:SecretKey"],
                new AmazonS3Config
                {
                    ServiceURL = configuration["Storage:Cloudflare:Endpoint"],
                    ForcePathStyle = true,
                }
            ));

            services.AddScoped<IStorageProvider>(service =>
            {
                var s3Client = service.GetRequiredService<IAmazonS3>();
                return new S3StorageProvider(s3Client);
            });

            services.AddScoped<IUrlProvider>(service =>
            {
                var s3Client = service.GetRequiredService<IAmazonS3>();
                return new S3UrlProvider(s3Client);
            });

            return services;
        }

        private IServiceCollection AddStorageServices(IConfiguration configuration)
        {
            var bucketName =
                configuration["Storage:Buckets:Photos"]
                ?? throw new InvalidOperationException("Storage:Buckets:Photos is required.");

            services.AddScoped<IPhotoStorageService>(service =>
            {
                var storageProvider = service.GetRequiredService<IStorageProvider>();
                return new PhotoStorageService(storageProvider, bucketName);
            });

            services.AddScoped<IPhotoUrlService>(service =>
            {
                var urlProvider = service.GetRequiredService<IUrlProvider>();
                return new PhotoUrlService(urlProvider, bucketName);
            });
            return services;
        }
    }
}
