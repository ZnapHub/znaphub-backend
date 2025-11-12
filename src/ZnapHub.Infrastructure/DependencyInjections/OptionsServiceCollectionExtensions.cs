using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Shared.Options;

namespace ZnapHub.Infrastructure.DependencyInjections;

public static class OptionsServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddZnapHubOptions(IConfiguration configuration)
        {
            services.Configure<StorageOptions>(configuration.GetSection("Storage"));
            services.AddOptions<StorageOptions>().Bind(configuration.GetSection("Storage"));

            return services;
        }
    }
}
