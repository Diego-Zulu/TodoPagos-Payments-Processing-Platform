using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Hashing
    {

        private const int SALT_LENGTH = 32 * UnicodeEncoding.CharSize;

        private const string TO_BE_USED_HASH_ALGORITHM = "SHA256";

        public static byte[] HashValue(string value, byte[] salt)
        {
            HashAlgorithm hash = HashAlgorithm.Create(TO_BE_USED_HASH_ALGORITHM);
            byte[] passInBytes = Encoding.Unicode.GetBytes(value);

            byte[] hashedPassword = hash.ComputeHash(passInBytes);
            IEnumerable<byte> saltAndHashedPassword = salt.Concat(hashedPassword);

            return saltAndHashedPassword.ToArray();

        }

        public static byte[] GetRandomSalt()
        {
            var salt = new byte[SALT_LENGTH];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public static bool VerifyHash(string source, byte[] hashedValueSalt, byte[] hashedValue)
        {
            byte[] hashedSource = Hashing.HashValue(source, hashedValueSalt);
            string holi = new string (Encoding.Unicode.GetChars(hashedSource));
            return hashedSource.SequenceEqual(hashedValue);
        }
    }
}
