using AzureDatabase.Services;
using Cors;
using Common.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.AddCorsPolicy();
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddControllers();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseCors("Cors");
app.MapControllers();
app.Run();
