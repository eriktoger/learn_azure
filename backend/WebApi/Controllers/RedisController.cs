using Common.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Redis.Services;
using StackExchange.Redis;

namespace Redis.Controllers
{
    [ApiController]
    [Route("/redis")]
    public class RedisController(ILogger<RedisController> logger, TelemetryClient telemetryClient, IRedisService redisService) : Controller
    {
        private readonly ILogger<RedisController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;
        private readonly IRedisService _redisService = redisService;

        [HttpGet(Name = "GetRedis")]
        public IActionResult Get()
        {
            _logger.LogInformation("a 'Redis get' was requested");
            _telemetryClient.TrackEvent("a 'Redis get' was requested");

            var value = _redisService.GetValue();

            return Ok(value.ToString());
        }
    }
}