var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env ?? "Development"}.json", optional: true, reloadOnChange: true);
var allowedOrigins = builder.Configuration["AllowedOrigins"];

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Cors",
                      policy =>
                      {
                          policy.WithOrigins(allowedOrigins ?? "")
                                .AllowAnyHeader()
                                .WithMethods("GET", "OPTIONS")
                                .WithExposedHeaders("Access-Control-Allow-Origin"); ;
                      });
});

var app = builder.Build();

app.UseCors("Cors");
app.MapControllers();
app.Run();
