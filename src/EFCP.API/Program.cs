using Carter;
using EFCP.Application;
using EFCP.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();
