using Common.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using StackExchange.Redis;

namespace Redis.Controllers
{
    [ApiController]
    [Route("/redis")]
    public class RedisController(ILogger<RedisController> logger, TelemetryClient telemetryClient, IConfigurationService configurationService) : Controller
    {
        private readonly ILogger<RedisController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;
        private readonly IConfigurationService _configurationService = configurationService;


        [HttpGet(Name = "GetRedis")]
        public async Task<IActionResult> Get([FromQuery] string? filename)
        {
            _logger.LogInformation("a 'Redis get' was requested");
            _telemetryClient.TrackEvent("a 'Redis get' was requested");

            var connectionString = _configurationService.GetSecretRedisConnectionString();
            var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);
            var database = connection.GetDatabase();
            var value = database.StringGet("value");

            if (value.ToString() == "")
            {
                var rand = new Random();
                var randomNumber = rand.Next(0, 100);
                var expireTime = new TimeSpan(0, 5, 0);
                database.StringSet("value", randomNumber, expireTime);
                _logger.LogInformation("Value is empty, and is now set to a random value");
                _telemetryClient.TrackEvent("Value is empty, and is now set to a random value");
                var newValue = database.StringGet("value").ToString();
                return Ok(newValue);
            }

            return Ok(value.ToString());
        }
    }
}