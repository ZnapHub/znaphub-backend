using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Infrastructure.Features.Photos.Services;
using ZnapHub.Infrastructure.Storage.Interfaces;
using ZnapHub.Infrastructure.Storage.Providers;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class StorageServiceCollectionExtensions
{
    internal static IServiceCollection AddStorage(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddS3Storage(configuration).AddStorageServices(configuration);

    private static IServiceCollection AddS3Storage(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IAmazonS3>(_ => new AmazonS3Client(
            configuration["Storage:Cloudflare:AccessKey"],
            configuration["Storage:Cloudflare:SecretKey"]
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

    private static IServiceCollection AddMinioStorage(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IMinioClient>(_ =>
            new MinioClient()
                .WithEndpoint(configuration["Storage:Minio:Endpoint"])
                .WithCredentials(
                    configuration["Storage:Minio:AccessKey"],
                    configuration["Storage:Minio:SecretKey"]
                )
                .WithSSL(Convert.ToBoolean(configuration["Storage:Minio:UseSsl"]))
                .Build()
        );

        services.AddScoped<IStorageProvider>(service =>
        {
            var minioClient = service.GetRequiredService<IMinioClient>();
            return new MinioStorageProvider(minioClient);
        });

        services.AddScoped<IUrlProvider>(service =>
        {
            var minioClient = service.GetRequiredService<IMinioClient>();
            return new MinioUrlProvider(minioClient);
        });

        return services;
    }

    private static IServiceCollection AddStorageServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
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
