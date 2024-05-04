using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;

namespace HelloWorld.Controllers
{
    [ApiController]
    [Route("/helloworld")]
    public class HelloWorldController(ILogger<HelloWorldController> logger, TelemetryClient telemetryClient) : Controller
    {
        private readonly ILogger<HelloWorldController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;

        [HttpGet(Name = "GetHelloWorld")]
        public IActionResult Get()
        {
            _logger.LogInformation("a 'Hello world get' was requested");
            _telemetryClient.TrackEvent("a 'Hello world get' was requested (Application insights version)");

            return Ok("Hello, World! (from Backend)");
        }
    }
}