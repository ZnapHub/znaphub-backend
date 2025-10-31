using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Identity;
using ZnapHub.Infrastructure.Identity;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class AuthServiceCollectionExtensions
{
    internal static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}
