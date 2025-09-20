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
                .WithEndpoint(configuration["Minio:Endpoint"])
                .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .WithSSL(Convert.ToBoolean(configuration["Minio:Secure"]))
                .Build()
        );

        services.AddScoped<IStorageService>(service =>
        {
            var minioClient = service.GetRequiredService<IMinioClient>();
            var bucketName =
                configuration["Minio:Bucket"]
                ?? throw new InvalidOperationException("Minio:Bucket is required.");
            return new MinioStorageService(minioClient, bucketName);
        });
        return services;
    }
}
