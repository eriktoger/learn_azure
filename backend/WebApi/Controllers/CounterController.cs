using Microsoft.AspNetCore.Mvc;
using Common.Services;
using AzureDatabase.Services;

namespace Counter.Controllers
{
    [ApiController]
    [Route("/counter")]
    public class CounterController(ILogger<CounterController> logger, IConfigurationService configurationService, IStatisticService statisticService) : Controller
    {
        private readonly ILogger<CounterController> _logger = logger;
        private readonly IConfigurationService _configurationService = configurationService;

        [HttpGet(Name = "GetCounter")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Counter call made");

            var id = _configurationService.GetStatisticId();
            var resource = await statisticService.GetStatisticById(id);
            if (resource == null)
            {
                return Ok($"Counter not found");
            }

            return Ok($"Counter: {resource.Counter}");
        }
    }
}