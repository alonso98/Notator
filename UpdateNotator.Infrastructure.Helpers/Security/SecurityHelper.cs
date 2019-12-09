using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UpdateNotator.Infrastructure.Helpers.Security
{
    public static class SecurityHelper
    {
        private static int iterationCount = 10000;

        public static SecurityHelperResult CreatePasswordHash(string password)
        {
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return CreatePasswordHash(password, salt);
        }
        public static SecurityHelperResult CreatePasswordHash(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password,
                                                salt,
                                                iterationCount,
                                                HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] resultPasswordBytes = new byte[36];
            Array.Copy(salt, 0, resultPasswordBytes, 0, 16);
            Array.Copy(hash, 0, resultPasswordBytes, 16, 20);

            string resultPassword = Convert.ToBase64String(resultPasswordBytes);
            string resultSalt = Convert.ToBase64String(salt);
            return new SecurityHelperResult(resultPassword, resultSalt);
        }
        public static SecurityHelperResult CreatePasswordHash(string password, string salt)
        {
            var byteSalt = Convert.FromBase64String(salt);
            return CreatePasswordHash(password, byteSalt);
        }
    }
}
