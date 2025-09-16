using EventFlow.Application.Abstractions.Data;
using EventFlow.Domain.Repositories;
using EventFlow.Infrastructure.Persistence;
using EventFlow.Infrastructure.Persistence.Contexts;
using EventFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventFlow.Infrastructure.DependencyInjections.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddDbContexts(configuration).AddUnitOfWork().AddRepositories();
}
