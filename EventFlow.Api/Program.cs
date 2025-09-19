using EventFlow.Infrastructure.Data.Contexts;
using EventFlow.Infrastructure.DependencyInjections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEventFlow(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EventFlowWriteContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();
