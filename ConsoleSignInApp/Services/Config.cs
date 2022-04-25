using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSignInApp.Services
{
    internal class Config
    {
        public const int SaltLength = 8;
        public const int HashIterations = 25;
        public const int nHash = 70;
        public const int PasswordLengthMin = 5;
        public const int PasswordLengthMax = 15;
        public const int UserIdLengthMin = 3;
        public const int UserIdLengthMax = 10;
    }
}
