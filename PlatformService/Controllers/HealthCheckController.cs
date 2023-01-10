using Microsoft.AspNetCore.Mvc;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Get() => "Platform Service up and running";
    }
}