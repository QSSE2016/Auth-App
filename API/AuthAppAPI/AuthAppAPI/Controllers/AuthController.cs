using Microsoft.AspNetCore.Mvc;

namespace AuthAppAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        public AuthController() { }


        [HttpGet("cookies")]
        public IActionResult CookiesAuth()
        {
            string? token = Request.Cookies["AuthCookie"];
            if (token == null)
                return Unauthorized();

            return Ok();
        }

        [HttpGet("jwt")]
        public IActionResult JwtAuth()
        {
            return Ok();
        }
    }
}
