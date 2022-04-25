using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Infrastructure
{
    public interface IDataStoreCommands
    {
        bool AddUser(UserCredentials user);
        bool UpdateUser(UserCredentials userCredentialsOld, UserCredentials userCredentialsNew);

        bool SessionInsert(string Id);
        bool SessionRemove(string UserId);
    }
}