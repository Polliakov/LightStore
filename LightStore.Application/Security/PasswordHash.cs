using System;
using System.Security.Cryptography;
using System.Text;

namespace LightStore.Application.Security
{
    public static class PasswordHash
    {
        public const int PasswordMaxLength = 32;
        public const int PasswordHashLength = 72;
        public const int SaltLength = 8;

        private const int roundCount = 10000;

        public static byte[] Calculate(string password)
        {
            #region Exceptions
            if (password is null)
                throw new ArgumentNullException(nameof(password));
            if (password.Length > PasswordMaxLength)
                throw new ArgumentOutOfRangeException(nameof(password), $"Max length is {PasswordMaxLength}.");
            #endregion

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltLength];
            rng.GetBytes(salt);

            var hash = CalculateHash(password, salt);
            return Concat(salt, hash);
        }

        public static bool CheckEqual(string password, byte[] passwordHash)
        {
            #region Exceptions
            if (password is null)
                throw new ArgumentNullException(nameof(password));
            if (passwordHash is null)
                throw new ArgumentNullException(nameof(passwordHash));
            if (passwordHash.Length != PasswordHashLength)
                throw new ArgumentException($"Argument \"{nameof(passwordHash)}\" must has length {PasswordHashLength}.");
            if (password.Length > PasswordMaxLength)
                throw new ArgumentOutOfRangeException(nameof(password), $"Max length is {PasswordMaxLength}.");
            #endregion

            var salt = new byte[SaltLength];
            Buffer.BlockCopy(passwordHash, 0, salt, 0, SaltLength);

            var hash = CalculateHash(password, salt);
            return hash.AsSpan().SequenceEqual(passwordHash.AsSpan(salt.Length));
        }

        private static byte[] CalculateHash(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using var sha = new SHA512CryptoServiceProvider();

            var passwordHash = Concat(salt, passwordBytes);           
            for (int i = 0; i < roundCount; i++)
                passwordHash = sha.ComputeHash(passwordHash);

            return passwordHash;
        }

        private static byte[] Concat(byte[] first, byte[] second)
        {
            var result = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            return result;
        }
    }
}
