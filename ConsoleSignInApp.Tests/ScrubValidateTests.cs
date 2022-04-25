using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Services;
using NUnit.Framework;


namespace ConsoleSignInApp.Tests
{
    public class ScrubValidateTests
    {
        ScrubValidate scrub;
        [SetUp]
        public void Setup()
        {
            scrub = new ScrubValidate();
        }

        [Test]
        public void Test_Password_IsNull()
        {
            string Password;

            scrub.isValidPasswordLength(null);

            Assert.That(scrub.ValidateErrors.Count == 1);
        }

        [Test]
        public void Test_Password_Length_IsValid()
        {
            string PasswordMin = "12345";
            string PasswordMax = "012345678912345";

            scrub.isValidPasswordLength(PasswordMin);
            scrub.isValidPasswordLength(PasswordMax);

            Assert.That(scrub.ValidateErrors.Count == 0);
        }

        [Test]
        public void Test_Password_Length_IsInValid()
        {
            string PasswordShort = "1234";
            string PasswordLong = "01234567891234567";

            scrub.isValidPasswordLength(PasswordShort);
            scrub.isValidPasswordLength(PasswordLong);
            Assert.That(scrub.ValidateErrors.Count == 2);
        }

        [Test]
        public void Test_UserEntry_HasBadChars()
        {
            string BadInputValue = "p--BadWord";
            bool HasBadChars = false;

            HasBadChars = scrub.hasBadCharacters(BadInputValue);

            Assert.IsTrue(HasBadChars);
        }

        [Test]
        public void Test_UserEntry_HasNoBadChars()
        {
            string BadInputValue = "HasNoBadChars";
            bool HasBadChars = false;

            HasBadChars = scrub.hasBadCharacters(BadInputValue);

            Assert.IsFalse(HasBadChars);
        }

        [Test]
        public void Test_Password_Has_Upper_Lower_Chars()
        {
            string Password = "MyPassword123!";

            scrub.isValidPasswordFormat(Password);

            Assert.That(scrub.ValidateErrors.Count == 0);
        }

        [Test]
        public void Test_Passowrd_Is_Correct_Format()
        {
            string Password = "MyPassword123!";

            scrub.isValidPasswordFormat(Password);

            Assert.That(scrub.ValidateErrors.Count == 0);
        }

        [Test]
        public void Test_IsValidMethod_InvalidPassword()
        {
            string Password = "mypasswird";

            scrub.isValidPasswordFormat(Password);

            Assert.That(scrub.ValidateErrors.Count > 0);
        }

        [Test]
        public void Test_IsValidMethod_ValidPassword()
        {
            string Password = "MyPassword123";

            scrub.isValidPasswordFormat(Password);

            Assert.That(scrub.ValidateErrors.Count > 0);
        }
    }
}
