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

        public const int SALT_LENGTH = 32 * UnicodeEncoding.CharSize;

        private const string TO_BE_USED_HASH_ALGORITHM = "SHA256";

        public static string HashValue(string value, string salt)
        {
            HashAlgorithm hash = HashAlgorithm.Create(TO_BE_USED_HASH_ALGORITHM);
            byte[] passInBytes = Encoding.Unicode.GetBytes(value);

            byte[] hashedPassword = hash.ComputeHash(passInBytes);
            string hashedPassInString = new string(Encoding.Unicode.GetChars(hashedPassword));
            

            return salt + hashedPassInString;

        }

        public static string GetRandomSalt()
        {
            var salt = new byte[SALT_LENGTH];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return new string(Encoding.Unicode.GetChars(salt));
        }

        public static bool VerifyHash(string source, string hashedValueSalt, string hashedValue)
        {
            string hashedSource = Hashing.HashValue(source, hashedValueSalt);
            return hashedSource.Equals(hashedValue);
        }

        public static bool BothAreSaltsAndAreEqual (string oneSalt, string otherSalt)
        {
            return oneSalt.Length == SALT_LENGTH && otherSalt.Length == SALT_LENGTH && oneSalt.Equals(otherSalt);
        }

        public static string GetSaltFromPassword(string hashedPassword)
        {
            if (hashedPassword.Length <= SALT_LENGTH)
            {
                throw new ArgumentException();
            }

            return hashedPassword.Substring(0, SALT_LENGTH);
        }
    }
}
