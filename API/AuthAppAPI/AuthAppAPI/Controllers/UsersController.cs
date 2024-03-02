using AuthAppAPI.Models;
using AuthAppAPI.Models.DTOS;
using AuthAppAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AuthAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository repo;

        public UsersController(IUserRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto request)
        {
            User? savedUser = await repo.CreateAsync(request);
            return savedUser == null ? Conflict() : Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await repo.GetAll();
            List<UserDto> userDtos = users.ConvertAll<UserDto>(user => new UserDto()
            {
                Email = user.Email,
                HashedPassword = user.HashedPassword,
                Username = user.Username,
            });
            return Ok(userDtos);
        }
    }
}
