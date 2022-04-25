using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Infrastructure
{
    public interface IDataStoreQueries
    {
        bool ValidateUser(UserCredentials user);
    }
}