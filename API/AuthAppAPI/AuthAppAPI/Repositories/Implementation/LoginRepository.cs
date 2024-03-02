using AuthAppAPI.Data;
using AuthAppAPI.Models;
using AuthAppAPI.Models.DTOS;
using AuthAppAPI.Repositories.Interface;
using AuthAppAPI.Security;
using Microsoft.EntityFrameworkCore;

namespace AuthAppAPI.Repositories.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext context;
        private PasswordVerifier verifier;

        public LoginRepository(AppDbContext context, PasswordVerifier verifier)
        {
            this.context = context;
            this.verifier = verifier;
        }

        public async Task<LoginStatus> Login(LoginRequestDto request)
        {
            User? user = await context.Users.SingleOrDefaultAsync(us => us.Email == request.Email);
            if (user == null)
                return LoginStatus.UserNotFound;

            if (verifier.VerifyPassword(request.Password, user.HashedPassword, user.Salt))
                return LoginStatus.Success;
            else
                return LoginStatus.WrongPassword;
        }
    }
}
