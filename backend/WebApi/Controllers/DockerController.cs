using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;
using System.Text.Json;
using Common.Services;

namespace Docker.Controllers
{
    [ApiController]
    [Route("/docker")]
    public class DockerController(ILogger<DockerController> logger, TelemetryClient telemetryClient, IConfigurationService configurationService) : Controller
    {
        private readonly ILogger<DockerController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;

        private readonly IConfigurationService _configurationService = configurationService;

        static readonly HttpClient client = new();

        [HttpGet(Name = "GetDocker")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("a 'Docker get' was requested");
            _telemetryClient.TrackEvent("a 'Docker get' was requested (Application insights version)");

            var dockerUrl = _configurationService.GetDockerUrl();
            HttpResponseMessage response = await client.GetAsync(dockerUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Parse the JSON response
            var jsonDoc = JsonDocument.Parse(responseBody);
            JsonElement root = jsonDoc.RootElement;

            // Extract the "message" field
            if (root.TryGetProperty("message", out JsonElement messageElement))
            {
                string message = messageElement.GetString() ?? "No message found";
                return Ok(message);
            }

            return Ok("No message found in docker");
        }
    }
}