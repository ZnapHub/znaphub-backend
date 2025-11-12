using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class InfrastructureServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddInfrastructure(IConfiguration configuration) =>
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
}
