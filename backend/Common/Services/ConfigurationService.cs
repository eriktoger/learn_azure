using Microsoft.Extensions.Configuration;

namespace Common.Services;
public class ConfigurationService(IConfiguration configuration) : IConfigurationService
{
    private readonly IConfiguration _configuration = configuration;

    public string GetAllowedOrigins()
    {
        return _configuration["AllowedOrigins"] ?? "";
    }
    public string GetDatabaseConnectionString()
    {
        return _configuration["PRIMARY_CONNECTION_STRING"] ?? "";
    }

    public string GetStatisticId()
    {
        return _configuration["STATISTIC_ID"] ?? "";
    }

}


