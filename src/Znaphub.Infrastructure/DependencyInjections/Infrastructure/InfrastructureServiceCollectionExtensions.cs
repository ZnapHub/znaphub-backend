using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZnapHub.Infrastructurez.DependencyInjections.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    ) =>
        services
            .AddDbContexts(configuration)
            .AddUnitOfWork()
            .AddRepositories()
            .AddStorage(configuration)
            .AddHealthChecks(configuration);
}
