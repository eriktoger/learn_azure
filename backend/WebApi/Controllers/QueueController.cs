using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;
using Queue.Services;
using WebApi.Models;


namespace Queue.Controllers
{
    [ApiController]
    [Route("/queue")]
    public class QueueController(ILogger<QueueController> logger, TelemetryClient telemetryClient, IQueueService queueService) : Controller
    {
        private readonly ILogger<QueueController> _logger = logger;
        private readonly TelemetryClient _telemetryClient = telemetryClient;
        private readonly IQueueService _queueService = queueService;

        [HttpGet(Name = "GetQueue")]
        public async Task<IActionResult> Get([FromQuery] bool? peek)
        {
            _logger.LogInformation("a 'Queue get' was requested");
            _telemetryClient.TrackEvent("a 'Queue get' was requested (Application insights version)");
            var message = await _queueService.GetMessage(peek ?? false);
            if (message == null)
            {
                return NotFound("Queue is empty");
            }
            return Ok(message);
        }

        [HttpPost(Name = "PostQueue")]
        public IActionResult Post([FromBody] Message message)
        {
            _logger.LogInformation("a 'Queue post' was requested");
            _telemetryClient.TrackEvent("a 'Queue post' was requested (Application insights version)");
            _queueService.SendMessage(message.Content);
            return Ok("Message sent");
        }
    }
}