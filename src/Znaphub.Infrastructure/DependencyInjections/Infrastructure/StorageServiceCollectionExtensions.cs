using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Infrastructure.Storage;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(
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
                .WithSSL(Convert.ToBoolean(configuration["Storage:Minio:Secure"]))
                .Build()
        );

        services.AddScoped<IStorageService>(service =>
        {
            var minioClient = service.GetRequiredService<IMinioClient>();
            var bucketName =
                configuration["Storage:Buckets:Photos"]
                ?? throw new InvalidOperationException("Storage:Buckets:Photos is required.");
            return new MinioStorageService(minioClient, bucketName);
        });

        services.AddScoped<IUrlService>(service =>
        {
            var minioClient = service.GetRequiredService<IMinioClient>();
            var bucketName =
                configuration["Storage:Buckets:Photos"]
                ?? throw new InvalidOperationException("Storage:Buckets:Photos is required.");
            return new MinioUrlService(minioClient, bucketName);
        });
        return services;
    }
}
