namespace AuthAppAPI.Models.DTOS
{
    public class UserDto
    {
        // Normally you don't return the hashed password too. that's a breach of security right there
        // But the purpose of this project is to understand the process, not caring about basic security facts that even Joe Biden knows.

        public string Username { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
