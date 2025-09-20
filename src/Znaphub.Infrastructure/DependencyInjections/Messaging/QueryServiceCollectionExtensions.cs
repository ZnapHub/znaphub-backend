using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Znaphub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Infrastructurez.Dispatchers;

namespace ZnapHub.Infrastructurez.DependencyInjections.Messaging;

public static class QueryServiceCollectionExtensions
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

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
