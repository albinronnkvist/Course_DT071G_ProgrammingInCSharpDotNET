using System.Security.Cryptography;
using System.Text;

namespace ForumAPI.UserSecurity
{
    public static class SecurePassword
    {
        public static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[32];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            
            return salt;
        }

        private static byte[] SaltAndHashPassword(string password, byte[] salt)
        {
            SHA512 sha = SHA512.Create();

            string saltedPassword = password + salt;

            return sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword));
        }
    }
}