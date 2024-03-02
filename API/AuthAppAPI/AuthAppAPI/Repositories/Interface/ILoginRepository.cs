using AuthAppAPI.Models.DTOS;

namespace AuthAppAPI.Repositories.Interface
{
    public interface ILoginRepository
    {
        Task<LoginStatus> Login(LoginRequestDto request);
    }
}
