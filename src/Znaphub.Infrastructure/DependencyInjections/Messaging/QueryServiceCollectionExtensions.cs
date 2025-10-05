using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Infrastructure.Dispatchers;

namespace ZnapHub.Infrastructure.DependencyInjections.Messaging;

public static class QueryServiceCollectionExtensions
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var assembly = typeof(IQuery).Assembly;

        services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        services.Scan(s =>
            s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
