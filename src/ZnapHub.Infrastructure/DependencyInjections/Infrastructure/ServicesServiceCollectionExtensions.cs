using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Domain.Features.QrCodes.Services;
using ZnapHub.Infrastructure.Features.QrCodes.Services;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class ServicesServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddServices()
        {
            services.AddScoped<IShortIdGenerator, ShortIdGenerator>();
            return services;
        }
    }
}
