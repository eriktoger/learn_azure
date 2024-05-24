using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;
using Common.Services;

namespace Function.Controllers
{
    [ApiController]
    [Route("/function")]
    public class FunctionController(ILogger<FunctionController> logger, TelemetryClient telemetryClient, IConfigurationService configurationService) : Controller
    {
        private readonly ILogger<FunctionController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;
        private readonly IConfigurationService _configurationService = configurationService;
        static readonly HttpClient client = new();

        [HttpGet(Name = "GetFunction")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name)
        {
            _logger.LogInformation("a 'Function get' was requested");
            _telemetryClient.TrackEvent("a 'Function get' was requested (Application insights version)");

            var functionUrl = _configurationService.GetFunctionUrl();
            var functionCode = _configurationService.GetFunctionCode();

            var url = $"{functionUrl}?name={name ?? "No-name"}&code={functionCode}";
            HttpResponseMessage response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }
    }
}