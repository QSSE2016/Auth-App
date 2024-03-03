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
                    Path = "/",

                    // These two are necessary for blocking an error which stops the creation of this cookie
                    // Essentially because we are going from https to http (the cookie that is), the response is unsafe.
                    // For now there is a warning about this that in future Chrome versions this will result in an error
                    // Regardless know that these two should probably be "strict" and "true" for actual https applications.
                    SameSite = SameSiteMode.None,
                    Secure = true,

                    //
                    HttpOnly = true
                });
            }

            return StatusCode(200, new { loginResult = status });
        }
    }
}
