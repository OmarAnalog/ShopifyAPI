using Microsoft.AspNetCore.Mvc;

namespace Shopify.Presentation.Controllers
{
    public class ErrorController:ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() {
            Exception? context = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
            return Problem(detail:context?.Message);
        }
    }
}
