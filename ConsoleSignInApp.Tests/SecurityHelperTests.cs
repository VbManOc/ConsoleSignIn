using NUnit.Framework;
using ConsoleSignInApp.Services;

namespace ConsoleSignInApp.Tests
{
    public class SecurityHelperTests
    {

        [Test]
        public void Test_That_Salt_Is_Appropriate_Length()
        {
            var securityHelper = ServicesFactory.GetSecurityHelper(); ;
            int SaltLength = 8;

            string saltyString = securityHelper.GenerateSalt(SaltLength);

            Assert.Greater(saltyString.Length, SaltLength);
        }

        [Test]
        public void Test_Salt_Byte_Range_Correct()
        {
            var securityHelper = ServicesFactory.GetSecurityHelper();
            int SaltLengthZero = 0;
            int SaltLengthToBig = 350;

            string SaltTooSmall = securityHelper.GenerateSalt(SaltLengthZero);
            string SaltTooBig = securityHelper.GenerateSalt(SaltLengthToBig);

            Assert.True(SaltTooSmall.Length == 0);
            Assert.True(SaltTooBig.Length == 0);
        }

        [Test]
        public void Test_That_Password_Is_Hashed()
        {
            var securityHelper = ServicesFactory.GetSecurityHelper();
            string password = "password";
            int saltLength = 8;
            int iterations = 11;
            int nHash = 7;

            string passwordHashed = securityHelper.HashPassword(password,saltLength, iterations, nHash);

            Assert.AreNotEqual(passwordHashed, password); 
        }
    }
}