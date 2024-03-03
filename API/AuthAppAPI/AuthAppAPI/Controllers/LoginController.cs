using AuthAppAPI.Models.DTOS;
using AuthAppAPI.Repositories.Interface;
using AuthAppAPI.Security;
using Microsoft.AspNetCore.Mvc;

namespace AuthAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository repo;
        private readonly JwtGenerator generator;

        public LoginController(ILoginRepository repo,JwtGenerator generator)
        {
            this.repo = repo;
            this.generator = generator;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCredentials(LoginRequestDto request)
        {
            LoginStatus status = await repo.Login(request);
            if (status == LoginStatus.UserNotFound)
                return NotFound();
        
            // Set Cookie for web browser (localhost:4200 or in general the port for the angular app) , (for value im using a JWT token, prep for later)
            if(status == LoginStatus.Success)
            {
                string jwtToken = generator.Generate(request.Email);
                Response.Cookies.Append("AuthCookie", jwtToken, new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddSeconds(20),
                    Domain = "localhost",
                    //HttpOnly = true
                });
            }

            return StatusCode(200, new { loginResult = status });
        }
    }
}
