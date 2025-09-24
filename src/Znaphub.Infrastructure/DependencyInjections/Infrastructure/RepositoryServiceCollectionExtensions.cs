using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Domain.Events.Interfaces;
using ZnapHub.Domain.Photos.Interfaces;
using ZnapHub.Infrastructure.Data;
using ZnapHub.Infrastructure.Data.Events;
using ZnapHub.Infrastructure.Data.Photos;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class RepositoryServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPhotoWriteRepository, PhotoWriteRepository>();
        services.AddScoped<IPhotoReadRepository, PhotoReadRepository>();

        services.AddScoped<IEventWriteRepository, EventWriteRepository>();
        services.AddScoped<IEventReadRepository, EventReadRepository>();
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
