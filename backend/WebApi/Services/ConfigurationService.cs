namespace Services;
public class ConfigurationService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public string GetAllowedOrigins()
    {
        return _configuration["AllowedOrigins"] ?? "";
    }
}