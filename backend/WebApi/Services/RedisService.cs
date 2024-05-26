using Common.Services;
using Microsoft.ApplicationInsights;
using StackExchange.Redis;

namespace Redis.Services;
public class RedisService : IRedisService
{
    private readonly ILogger<RedisService> _logger;
    private readonly TelemetryClient _telemetryClient;
    private readonly IConfigurationService _configurationService;
    private readonly IDatabase _redisDatabase;

    public RedisService(ILogger<RedisService> logger, TelemetryClient telemetryClient, IConfigurationService configurationService)
    {
        _logger = logger;
        _configurationService = configurationService;
        _telemetryClient = telemetryClient;

        var connectionString = _configurationService.GetSecretRedisConnectionString();
        var connection = ConnectionMultiplexer.Connect(connectionString);
        _redisDatabase = connection.GetDatabase();

    }
    public string GetValue()
    {
        var value = _redisDatabase.StringGet("value").ToString();

        if (value == "")
        {
            var rand = new Random();
            var randomNumber = rand.Next(0, 100);
            var expireTime = new TimeSpan(0, 5, 0);
            _redisDatabase.StringSet("value", randomNumber, expireTime);
            _logger.LogInformation("Value was empty and new value created");
            _telemetryClient.TrackEvent("Value was empty and new value created");

            return _redisDatabase.StringGet("value").ToString();
        }

        return value;
    }
}


