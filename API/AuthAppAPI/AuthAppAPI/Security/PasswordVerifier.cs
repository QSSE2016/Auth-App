using System.Security.Cryptography;

namespace AuthAppAPI.Security
{
    public class PasswordVerifier : EncryptionInfo
    {
        public bool VerifyPassword(string password,string hash, byte[] salt) 
        { 
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password,salt,Iterations,HashAlgo,KeySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash)); // fixed time, so timing exploits are mitigated.
        }
    }
}
