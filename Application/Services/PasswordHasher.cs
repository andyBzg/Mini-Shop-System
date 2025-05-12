using Application.Interfaces;
using System.Security.Cryptography;

namespace Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int Iterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public string HashPassword(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
                throw new ArgumentException("Password cannot be null or empty", nameof(plainPassword));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string plainPassword, string storedPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
                throw new ArgumentException("Password cannot be null or empty", nameof(plainPassword));

            if (string.IsNullOrEmpty(storedPassword))
                throw new ArgumentException("Hashed Password cannot be null or empty", nameof(storedPassword));

            byte[] hashBytes = Convert.FromBase64String(storedPassword);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hash[i] != hashBytes[i + SaltSize])                
                    return false;            
            }
            return true;
        }
    }
}
