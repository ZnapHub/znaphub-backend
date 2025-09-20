using Microsoft.Extensions.DependencyInjection;
using Znaphub.Application.Abstractions.Data;
using Znaphub.Domain.Photos;
using ZnapHub.Infrastructurez.Data;
using ZnapHub.Infrastructurez.Data.Repositories;

namespace ZnapHub.Infrastructurez.DependencyInjections.Infrastructure;

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
