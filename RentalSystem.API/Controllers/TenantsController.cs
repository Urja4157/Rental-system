using Microsoft.AspNetCore.Mvc;

namespace RentalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test successful.");
        }
    }
}
