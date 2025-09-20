using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Infrastructure.Dispatchers;

namespace ZnapHub.Infrastructure.DependencyInjections.Messaging;

public static class CommandServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
        services.Scan(s =>
            s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
        return services;
    }
}
