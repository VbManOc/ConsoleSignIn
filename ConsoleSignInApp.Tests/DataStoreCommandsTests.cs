using NUnit.Framework;
using ConsoleSignInApp.Entities;
using ConsoleSignInApp.Infrastructure;

namespace ConsoleSignInApp.Tests
{
    public class DataStoreCommandsTests
    {
        bool result;

        UserCredentials user;

        [SetUp]
        public void Setup_Fake_UserRecord()
        {
            user = new UserCredentials();
            user.UserId = "JohnDoe";
            user.Password = "password";
        }

        [Test]
        public void Test_Only_One_Instance_Of_DataStore()
        {
            DataStore sut1 = DataStore.Instance();
            DataStore sut2 = DataStore.Instance();
            bool SameInstance = false;

            SameInstance = (sut1 == sut2);

            Assert.IsTrue(SameInstance);
            Assert.AreSame(sut1, sut2);
        }

        [Test]
        public void Test_User_Can_Be_Added()
        {
            DataStoreCommands DAL = new DataStoreCommands();

            result = DAL.AddUser(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Test_NotImplemented_Update_Fails()
        {
            DataStoreCommands DAL = new DataStoreCommands();

            UserCredentials userUpdate = new UserCredentials();
            userUpdate.UserId = "JohnDoe";
            userUpdate.Password = "newPassword";

            result = DAL.UpdateUser(user, userUpdate);

            Assert.That(result, Is.False);
        }

        [TestCase]
        public void Test_Insert_Get_User()
        {
            DataStoreCommands DalIn = new DataStoreCommands();
            DataStoreQueries DalOut = new DataStoreQueries();

            bool resultInsert = false;
            bool resultGet = false;

            resultInsert = DalIn.AddUser(user);
            resultGet = DalOut.ValidateUser(user);

            Assert.That(resultInsert, Is.True);
            Assert.That(resultGet, Is.True);

        }
    }
}
