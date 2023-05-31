using System.Security.Cryptography;

namespace BreweryWholesale.Infrastructure.Utilities
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = rfc2898DeriveBytes.GetBytes(HashSize);

                // Combine salt and hash into a single byte array
                byte[] saltedHash = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, saltedHash, 0, SaltSize);
                Array.Copy(hash, 0, saltedHash, SaltSize, HashSize);

                // Convert the byte array to a Base64-encoded string
                return Convert.ToBase64String(saltedHash);
            }
        }
        
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] saltedHash = Convert.FromBase64String(hashedPassword);

            // Extract the salt and hash from the stored value
            byte[] salt = new byte[SaltSize];
            byte[] hash = new byte[HashSize];
            Array.Copy(saltedHash, 0, salt, 0, SaltSize);
            Array.Copy(saltedHash, SaltSize, hash, 0, HashSize);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] newHash = rfc2898DeriveBytes.GetBytes(HashSize);

                // Compare the new hash with the stored hash
                return AreByteArraysEqual(hash, newHash);
            }
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(salt);
            }
            return salt;
        }

        private static bool AreByteArraysEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
