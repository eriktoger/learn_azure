namespace Common.Services;
public interface IConfigurationService
{
    public string GetAllowedOrigins();
    public string GetDatabaseConnectionString();
    public string GetStatisticId();
    public string getDockerUrl();
    public string getFunctionUrl();
    public string getFunctionCode();
}