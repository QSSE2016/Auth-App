using AuthAppAPI.Models;
using AuthAppAPI.Models.DTOS;

namespace AuthAppAPI.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User?> CreateAsync(CreateUserRequestDto payload);
    }
}
