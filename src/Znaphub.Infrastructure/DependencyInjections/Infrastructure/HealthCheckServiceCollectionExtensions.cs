using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ZnapHub.Infrastructurez.DependencyInjections.Infrastructure;

public static class HealthCheckServiceCollectionExtensions
{
    public static IServiceCollection AddHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddHealthChecks()
            .AddNpgSql(
                configuration.GetConnectionString("EventFlowContext")
                    ?? throw new InvalidOperationException("EventFlowContext not found"),
                name: "Postgres",
                healthQuery: "SELECT 1;",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "sql"]
            );
        return services;
    }
}
