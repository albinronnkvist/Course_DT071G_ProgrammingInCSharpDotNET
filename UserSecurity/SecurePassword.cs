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

        public static byte[] SaltAndHashPassword(string password, byte[] salt)
        {
            SHA512 sha = SHA512.Create();

            string saltedPassword = password + salt;

            return sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword));
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var computedHash = SaltAndHashPassword(password, passwordSalt);
            
            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}