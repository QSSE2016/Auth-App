namespace AuthAppAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;


        public byte[] Salt { get; set; } = null!;
    }
}
