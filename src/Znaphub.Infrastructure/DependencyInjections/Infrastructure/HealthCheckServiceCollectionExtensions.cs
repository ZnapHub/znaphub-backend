using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class HealthCheckServiceCollectionExtensions
{
    internal static IServiceCollection AddHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddHealthChecks()
            .AddNpgSql(
                configuration.GetConnectionString("ZnapHubContext")
                    ?? throw new InvalidOperationException("ZnapHubContext not found"),
                name: "Postgres",
                healthQuery: "SELECT 1;",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "sql"]
            );
        return services;
    }
}
