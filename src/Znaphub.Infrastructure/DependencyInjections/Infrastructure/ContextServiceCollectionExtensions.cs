using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class ContextServiceCollectionExtensions
{
    public static IServiceCollection AddDbContexts(
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
