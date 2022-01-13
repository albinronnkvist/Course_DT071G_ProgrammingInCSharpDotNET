using System.Security.Cryptography;
using System.Text;

namespace ForumAPI.UserSecurity
{
    public static class SecurePassword
    {
        // Generate a random byte array that represents a salt value
        public static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[32];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            
            return salt;
        }


        // Salt and hash a password.
        // Pass the input password and the salt value to the method.
        public static byte[] SaltAndHashPassword(string password, byte[] salt)
        {
            // Concatenate the input password with the salt.
            string saltedPassword = password + salt;

            // Pass the salted password to a SHA512 hashing algorithm that generates a hash-value.
            SHA512 sha = SHA512.Create();
            return sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword));
        }


        // Verify a password
        // Pass the input password, a hashed password (from the database) and a salt value (from the database)
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Generate a new hash value with the input password and the salt from the database.
            var newHash = SaltAndHashPassword(password, passwordSalt);
            
            // Loop the entire length of the new hash value
            for(int i = 0; i < newHash.Length; i++)
            {
                // Compare the value of every index in both byte arrays(new hash and hash from the database)
                // If they do not match, return false.
                if(newHash[i] != passwordHash[i])
                {
                    return false;
                }
            }

            // If they match, return true.
            return true;
        }
    }
}