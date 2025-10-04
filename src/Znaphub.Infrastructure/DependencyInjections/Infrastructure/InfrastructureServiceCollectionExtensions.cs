using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class InfrastructureServiceCollectionExtensions
{
    internal static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    ) =>
        services
            .AddDbContexts(configuration)
            .AddUnitOfWork()
            .AddRepositories()
            .AddStorage(configuration)
            .AddHealthChecks(configuration)
            .AddFactories()
            .AddServices()
            .AddAuth();
}
