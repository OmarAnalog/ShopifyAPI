using Microsoft.AspNetCore.Mvc;

namespace Shopify.Presentation.Controllers
{
    [ApiController]
    [Route("Health")]
    public class HealthController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "ok", timestamp = System.DateTime.UtcNow });
    }
}
