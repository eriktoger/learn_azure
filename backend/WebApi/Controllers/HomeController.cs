using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        [HttpGet(Name = "GetHome")]
        public IActionResult Get()
        {
            _logger.LogInformation("a 'Home get' was requested");

            return Ok("Home!");
        }
    }
}