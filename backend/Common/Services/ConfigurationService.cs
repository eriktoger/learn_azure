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

    public string GetDockerUrl()
    {
        return _configuration["DOCKER_URL"] ?? "";
    }

    public string GetFunctionUrl()
    {
        return _configuration["FUNCTION_URL"] ?? "";
    }

    public string GetFunctionCode()
    {
        return _configuration["FUNCTION_CODE"] ?? "";
    }

    public string GetStorageContainer()
    {
        return _configuration["STORAGE_CONTAINER"] ?? "";
    }
    public string GetStorageQueueName()
    {
        return _configuration["STORAGE_QUEUE_NAME"] ?? "";
    }

    public string GetSecretBlobSaSToken()
    {
        return _configuration["BlobSasToken"] ?? "";
    }
    public string GetSecretRedisConnectionString()
    {
        return _configuration["RedisConnectionString"] ?? "";
    }
    public string GetSecretQueueConnectionString()
    {
        return _configuration["QueueConnectionString"] ?? "";
    }
}


