using EventFlow.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Infrastructure.DependencyInjections.Infrastructure;

public static class ContextServiceCollectionExtensions
{
    public static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<EventFlowWriteContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("EventFlowContext"))
        );
        services.AddDbContext<EventFlowReadContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("EventFlowContext"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );

        return services;
    }
}
