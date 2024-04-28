using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    [ApiController]
    [Route("/helloworld")]
    public class HelloWorldController(ILogger<HelloWorldController> logger) : Controller
    {
        private readonly ILogger<HelloWorldController> _logger = logger;

        [HttpGet(Name = "GetHelloWorld")]
        public IActionResult Get()
        {
            _logger.LogInformation("a 'Hello world get' was requested");

            return Ok("Hello, World! (from Backend)");
        }
    }
}