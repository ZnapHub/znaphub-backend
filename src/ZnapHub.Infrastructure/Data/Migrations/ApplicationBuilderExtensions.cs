using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Data.Migrations;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrationAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var appDb = scope.ServiceProvider.GetRequiredService<ZnapHubWriteContext>();
        await appDb.Database.MigrateAsync();
    }
}
