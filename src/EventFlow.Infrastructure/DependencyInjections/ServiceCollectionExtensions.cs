using EventFlow.Infrastructure.DependencyInjections.Infrastructure;
using EventFlow.Infrastructure.DependencyInjections.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Infrastructure.DependencyInjections;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventFlow(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddCommandHandlers().AddQueryHandlers().AddInfrastructure(configuration);
}
