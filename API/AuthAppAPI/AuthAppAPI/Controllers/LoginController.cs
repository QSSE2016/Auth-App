using AuthAppAPI.Models.DTOS;
using AuthAppAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AuthAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository repo;

        public LoginController(ILoginRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCredentials(LoginRequestDto request)
        {
            LoginStatus status = await repo.Login(request);
            if (status == LoginStatus.UserNotFound)
                return NotFound();


            return StatusCode(200, new { loginResult = status });
        }
    }
}
