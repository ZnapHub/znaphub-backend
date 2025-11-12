using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Infrastructure.Dispatchers;

namespace ZnapHub.Infrastructure.DependencyInjections.Messaging;

public static class CommandServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCommandHandlers()
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
}
