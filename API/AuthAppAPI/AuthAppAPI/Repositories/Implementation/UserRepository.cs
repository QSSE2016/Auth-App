using AuthAppAPI.Data;
using AuthAppAPI.Models;
using AuthAppAPI.Models.DTOS;
using AuthAppAPI.Repositories.Interface;
using AuthAppAPI.Security;

namespace AuthAppAPI.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        private PasswordHasher hasher;

        public UserRepository(AppDbContext context,PasswordHasher hasher)
        {
            this.context = context;
            this.hasher = hasher;
        }

        public async Task<User?> CreateAsync(CreateUserRequestDto payload)
        {
            if(context.Users.Any(us => us.Email == payload.Email))
                return null;

            User user = new User()
            {
                Username = payload.Username,
                Email = payload.Email,
                HashedPassword = hasher.HashPassword(payload.Password,out var salt),
                Salt = salt
            };

            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
