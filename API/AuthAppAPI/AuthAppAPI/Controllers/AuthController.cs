using Microsoft.AspNetCore.Authorization;
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
            Request.Cookies.TryGetValue("AuthCookie",out string? value);
            if (value == null)
                return Unauthorized();

            return Ok();
        }

        [Authorize]
        [HttpGet("jwt")]
        public IActionResult JwtAuth()
        {
            return Ok();
        }
    }
}
