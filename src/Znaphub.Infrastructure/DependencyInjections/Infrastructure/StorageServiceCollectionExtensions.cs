using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Infrastructure.Features.Photos;
using ZnapHub.Infrastructure.Features.Photos.Services;
using ZnapHub.Infrastructure.Storage.Interfaces;
using ZnapHub.Infrastructure.Storage.Providers;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddStorageProviders(configuration).AddStorageServices(configuration);

    private static IServiceCollection AddStorageProviders(
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
