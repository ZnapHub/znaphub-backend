using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructure.DependencyInjections.Infrastructure;
using ZnapHub.Infrastructure.DependencyInjections.Messaging;

namespace ZnapHub.Infrastructure.DependencyInjections;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZnapHub(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddCommandHandlers().AddQueryHandlers().AddInfrastructure(configuration);
}
