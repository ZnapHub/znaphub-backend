using EventFlow.Infrastructure.DependencyInjections.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Infrastructure.DependencyInjections;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventFlow(this IServiceCollection services)
    {
        services.AddCommandHandlers().AddQueryHandlers();

        return services;
    }
}
