using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Y_Chat.Middlewares;

namespace Y_Chat.Controllers
{
    [ProfileCheck]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController(ILogger<AuthController> logger) : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            logger.LogInformation("test route called");
            return Ok(new { message = "test" });
        }
    }
}
