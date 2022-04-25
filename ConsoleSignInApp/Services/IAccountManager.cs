using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Services
{
    public interface IAccountManager
    {
        bool AddUser(UserCredentials user);
    }
}