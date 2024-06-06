namespace Common.Services;
public interface IConfigurationService
{
    public string GetAllowedOrigins();
    public string GetDatabaseConnectionString();
    public string GetStatisticId();
    public string GetDockerUrl();
    public string GetFunctionUrl();
    public string GetFunctionCode();
    public string GetStorageQueueName();
    public string GetSecretBlobSaSToken();
    public string GetStorageContainer();
    public string GetSecretRedisConnectionString();
    public string GetSecretQueueConnectionString();
}