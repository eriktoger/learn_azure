using Common.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace File.Controllers
{
    [ApiController]
    [Route("/file")]
    public class FileController(ILogger<FileController> logger, TelemetryClient telemetryClient, IConfigurationService configurationService) : Controller
    {
        private readonly ILogger<FileController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;
        private readonly IConfigurationService _configurationService = configurationService;
        static readonly HttpClient client = new();

        [HttpGet(Name = "GetFile")]
        public async Task<IActionResult> Get([FromQuery] string? filename)
        {
            _logger.LogInformation("a 'File get' was requested");
            _telemetryClient.TrackEvent("a 'File get' was requested");

            var blobSaSToken = _configurationService.GetSecretBlobSaSToken();
            var storageContainer = _configurationService.GetStorageContainer();

            var url = $"{storageContainer}/{filename}?{blobSaSToken}";
            HttpResponseMessage response = await client.GetAsync(url);
            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
            return File(imageBytes, "image/jpeg");
        }
    }
}