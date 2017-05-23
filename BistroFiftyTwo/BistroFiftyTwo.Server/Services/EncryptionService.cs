using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace BistroFiftyTwo.Server.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string SlowOneWayHash(string clearText, string salt)
        {
            return
                Convert.ToBase64String(KeyDerivation.Pbkdf2(clearText, Convert.FromBase64String(salt),
                    KeyDerivationPrf.HMACSHA512, 10000, 256 / 8));
        }

        public string SlowOneWayHash(string clearText, byte[] salt)
        {
            return
                Convert.ToBase64String(KeyDerivation.Pbkdf2(clearText, salt, KeyDerivationPrf.HMACSHA512, 10000, 256 / 8));
        }

        public string GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}