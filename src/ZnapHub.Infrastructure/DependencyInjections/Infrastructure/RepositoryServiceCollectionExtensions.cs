using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Domain.Features.Events.Repositories;
using ZnapHub.Domain.Features.Photos.Repositories;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Infrastructure.Data;
using ZnapHub.Infrastructure.Features.Events.Data;
using ZnapHub.Infrastructure.Features.Photos.Data;
using ZnapHub.Infrastructure.Features.QrCodes.Data;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class RepositoryServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddRepositories()
        {
            services.AddScoped<IPhotoWriteRepository, PhotoWriteRepository>();
            services.AddScoped<IPhotoReadRepository, PhotoReadRepository>();

            services.AddScoped<IEventWriteRepository, EventWriteRepository>();
            services.AddScoped<IEventReadRepository, EventReadRepository>();

            services.AddScoped<IQrCodeReadRepository, QrCodeReadRepository>();
            services.AddScoped<IQrCodeWriteRepository, QrCodeWriteRepository>();
            return services;
        }

        internal IServiceCollection AddUnitOfWork()
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
