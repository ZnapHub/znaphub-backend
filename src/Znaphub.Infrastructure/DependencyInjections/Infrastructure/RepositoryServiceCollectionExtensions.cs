using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Domain.Photos;
using ZnapHub.Infrastructure.Data;
using ZnapHub.Infrastructure.Data.Repositories;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class RepositoryServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPhotoWriteRepository, PhotoWriteRepository>();
        services.AddScoped<IPhotoReadRepository, PhotoReadRepository>();
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
