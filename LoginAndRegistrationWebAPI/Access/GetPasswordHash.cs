using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LoginAndRegistrationWebAPI.Access
{
    internal static partial class AccessControl
    {
        private static string GetPasswordHash(string password, byte[] salt)
        {
            string passwordHash = "";

            try
            {
                passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2
                (
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                ));
            }
            catch { }

            return passwordHash;
        }



        private static byte[] GetSalt(int bytes)
        {
            byte[] salt = new byte[bytes];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
