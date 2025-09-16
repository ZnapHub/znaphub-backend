using EventFlow.Application.Abstractions.Data;
using EventFlow.Domain.Repositories;
using EventFlow.Infrastructure.Persistence;
using EventFlow.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Infrastructure.DependencyInjections.Infrastructure;

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
