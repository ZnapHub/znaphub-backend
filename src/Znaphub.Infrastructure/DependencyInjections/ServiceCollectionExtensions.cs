using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructurez.DependencyInjections.Infrastructure;
using ZnapHub.Infrastructurez.DependencyInjections.Messaging;

namespace ZnapHub.Infrastructurez.DependencyInjections;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventFlow(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddCommandHandlers().AddQueryHandlers().AddInfrastructure(configuration);
}
