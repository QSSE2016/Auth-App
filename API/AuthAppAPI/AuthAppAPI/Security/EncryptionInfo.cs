using System.Security.Cryptography;

namespace AuthAppAPI.Security
{
    public abstract class EncryptionInfo
    {
        protected int KeySize = 32;
        protected int Iterations = 100;
        protected HashAlgorithmName HashAlgo = HashAlgorithmName.SHA512;
    }
}
