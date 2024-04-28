namespace Cors;
using Services;

public static class Cors
{
    public static IServiceCollection AddCorsPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            ConfigurationService configurationService = new(builder.Configuration);
            var allowedOrigins = configurationService.GetAllowedOrigins();
            options.AddPolicy("Cors", policy =>
            {
                policy.WithOrigins(allowedOrigins ?? "")
                      .AllowAnyHeader()
                      .WithMethods("GET", "OPTIONS")
                      .WithExposedHeaders("Access-Control-Allow-Origin");
            });
        });

        return builder.Services;
    }
}