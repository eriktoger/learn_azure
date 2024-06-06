using AzureDatabase.Services;
using Cors;
using Common.Services;
using Redis.Services;
using Queue.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.AddCorsPolicy();
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddControllers();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

if (builder.Environment.IsProduction())
{
    var keyVaultUrl = builder.Configuration["KEY_VAULT_URL"];
    builder.Configuration.AddAzureKeyVault(keyVaultUrl);
}


builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
  {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      c.RoutePrefix = "swagger";
  });

app.UseCors("Cors");
app.MapControllers();
app.Run();
