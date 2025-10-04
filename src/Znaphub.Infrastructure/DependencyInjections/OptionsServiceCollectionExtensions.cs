using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Shared.Options;

namespace ZnapHub.Infrastructure.DependencyInjections;

public static class OptionsServiceCollectionExtensions
{
    public static IServiceCollection AddZnapHubOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<ApplicationOptions>(configuration);
        services.AddOptions<ApplicationOptions>().Bind(configuration);

        return services;
    }
}
