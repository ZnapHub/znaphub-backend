using Microsoft.EntityFrameworkCore;
using ZnapHub.Infrastructure.Data.Contexts;
using ZnapHub.Infrastructure.Identity;

namespace ZnapHub.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrationAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var appDb = scope.ServiceProvider.GetRequiredService<ZnapHubWriteContext>();
        await appDb.Database.MigrateAsync();
    
        var authDb = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        await authDb.Database.MigrateAsync();
    }
}