using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class ContextServiceCollectionExtensions
{
    internal static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ZnapHubWriteContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("ZnapHubContext"))
        );
        services.AddDbContext<ZnapHubReadContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("ZnapHubContext"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        return services;
    }
}
