using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Domain.Features.QrCodes.Factories;
using ZnapHub.Infrastructure.Features.QrCodes.Factories;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class FactoryServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddFactories()
        {
            services.AddScoped<IQrCodeUrlFactory, QrCodeUrlFactory>();
            return services;
        }
    }
}
