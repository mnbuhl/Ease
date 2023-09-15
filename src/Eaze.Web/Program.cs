using Eaze.Web;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.MapRequestPipeline();

app.Run();