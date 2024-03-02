using System.Security.Cryptography;
using System.Text;

namespace AuthAppAPI.Security
{
    public class PasswordHasher : EncryptionInfo
    {
        // These values are arbitrary just picking something that works quickly.

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(KeySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),salt,100,HashAlgo, KeySize);

            return Convert.ToHexString(hash);
        }
    }
}
