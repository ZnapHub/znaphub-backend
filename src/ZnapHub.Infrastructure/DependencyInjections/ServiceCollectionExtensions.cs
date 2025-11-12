using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructure.DependencyInjections.Infrastructure;
using ZnapHub.Infrastructure.DependencyInjections.Messaging;

namespace ZnapHub.Infrastructure.DependencyInjections;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddZnapHub(IConfiguration configuration) =>
            services
                .AddZnapHubOptions(configuration)
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddInfrastructure(configuration);
    }
}
