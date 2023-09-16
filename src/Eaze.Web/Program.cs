using Eaze.Infrastructure.Data;
using Eaze.Web;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

var databaseManager = new DatabaseManager(app.Services);
databaseManager.Migrate();

if (args.Contains("--seed"))
{
    databaseManager.Seed();
    return;
}

app.MapRequestPipeline();

app.Run();