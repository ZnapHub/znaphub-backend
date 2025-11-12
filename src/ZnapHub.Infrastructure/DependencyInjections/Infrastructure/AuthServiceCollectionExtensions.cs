using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Identity;
using ZnapHub.Infrastructure.Identity;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class AuthServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddAuth()
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
