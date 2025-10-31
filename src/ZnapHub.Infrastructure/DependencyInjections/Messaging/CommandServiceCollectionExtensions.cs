using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Infrastructure.Dispatchers;

namespace ZnapHub.Infrastructure.DependencyInjections.Messaging;

public static class CommandServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var assembly = typeof(ICommand).Assembly;

        services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
        services.Scan(s =>
            s.FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
        return services;
    }
}
