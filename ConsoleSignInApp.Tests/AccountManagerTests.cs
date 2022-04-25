using NUnit.Framework;
using Moq;
using ConsoleSignInApp.Entities;
using ConsoleSignInApp.Services;

namespace ConsoleSignInApp.Tests
{
    public class AccountManagerTests
    {
        bool result;

        UserCredentials user;
        AccountManager accountManager;
        AuthenticationService authenticationService;

        [SetUp]
        public void Setup_Fake_UserRecord()
        {
            user = new UserCredentials();
            user.UserId = "JohnDoe";
            user.Password = "Password0!";

            accountManager = new AccountManager(
                ServicesFactory.GetSecurityHelper(),
                ServicesFactory.GetScrubValidate(),
                ServicesFactory.GetDataStoreCommands()
                );

            authenticationService = new AuthenticationService(
                ServicesFactory.GetSecurityHelper(),
                ServicesFactory.GetScrubValidate(),
                ServicesFactory.GetDataStoreQueries()
                );
        }

        [Test]
        public void Test_ThrowsError_Properly()
        {
            //AccountManager accountManager = new AccountManager();
            UserCredentials nullUser = new UserCredentials();

            result = accountManager.AddUser(nullUser);

            Assert.IsFalse(result); 
        }

        [TestCase]
        public void Test_User_Is_Added()
        {
            //AccountManager accountManager = new AccountManager();

            result = accountManager.AddUser(user);

            Assert.IsTrue(accountManager.ErrorMessages == null);
            Assert.IsTrue(result);
        }

        [TestCase]
        public void Test_User_Is_NotAdded_OnBadUserId()
        {
            //AccountManager accountManager = new AccountManager();
            user = new UserCredentials();
            user.UserId = "badword_fido";
            user.Password = "pasrd123!";

            result = accountManager.AddUser(user);

            Assert.IsFalse(accountManager.ErrorMessages != null);
            Assert.IsFalse(result);
        }

        [TestCase]
        public void Test_DataStores_IsPopulated()
        {
            //AccountManager accountManager = new AccountManager();
            bool successful = false;

            successful = accountManager.PopulateWithRecords();

            Assert.IsTrue(successful);
            
        }

        [TestCase]
        public void Test_AddUsers_AuthenticateUser()
        {
            
            //AccountManager accountManager = new AccountManager();
            //AuthenticationService authenticationService = new AuthenticationService();  
            UserCredentials testUser = new UserCredentials();

            testUser.UserId = "Oracle";
            testUser.Password = "Th3M@tr!x";
            bool resultUsersAdd = false;
            bool resultInsert = false;
            bool resultGet = false;

            resultUsersAdd = accountManager.PopulateWithRecords();
            resultInsert = accountManager.AddUser(testUser);
            resultGet = authenticationService.AuthenticateUser(testUser);

            Assert.IsTrue(resultUsersAdd);
            Assert.IsTrue(resultInsert);
            Assert.IsTrue(resultGet);
        }
    }
}
