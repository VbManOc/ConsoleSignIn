using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleSignInApp.Services
{
    public class SecurityHelper : ISecurityHelper
    {
        static SecurityHelper instance;
        private string Salt = string.Empty;

        private SecurityHelper()
        {
            //"This is the constructor." - Morpheus
        }

        public static SecurityHelper Instance()
        {
            if (instance == null)
            {
                instance = new SecurityHelper();
            }
            return instance;
        }

        public string GenerateSalt(int SaltLength)
        {
            if (SaltLength <= 0 || SaltLength > 255) return String.Empty;

            byte[] saltbytes = new byte[SaltLength];
            RandomNumberGenerator RndGen = RandomNumberGenerator.Create();
            RndGen.GetBytes(saltbytes);

            return Convert.ToBase64String(saltbytes);
        }

        public string GetSalt(int SaltLength)
        {
            if (Salt.Length <= 0)
            {
                Salt = GenerateSalt(SaltLength);
            }
            return Salt;
        }

        public string HashPassword(string Password, int SaltLength, int Iterations, int nHash)
        {
            string salt = GetSalt(SaltLength);
            var saltyBytes = Convert.FromBase64String(salt);

            using (var rfcDeriveBytes = new Rfc2898DeriveBytes(Password, saltyBytes, Iterations))
            {
                return Convert.ToBase64String(rfcDeriveBytes.GetBytes(nHash));
            }
        }

    }
}
