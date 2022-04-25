using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Infrastructure;
using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Services
{
    public class AccountManager : IAccountManager
    {
        ISecurityHelper SecurityHelper;
        IDataStoreCommands DataStoreCommands;
        IScrubValidate ScrubValidate;

        private List<string> UserErrors;

        public List<string> ErrorMessages
        {
            get
            {
                return this.UserErrors;
            }
        }

        public AccountManager(ISecurityHelper securityHelper,IScrubValidate scrubValidate, IDataStoreCommands dataStoreCommands)
        {
            this.SecurityHelper = securityHelper;
            this.ScrubValidate = scrubValidate;
            this.DataStoreCommands = dataStoreCommands;
        }

        public bool UserAdd(string UserId, string UserPassword)
        {
            UserCredentials newUser = new UserCredentials();
            newUser.UserId = UserId;
            newUser.Password = UserPassword;

            return AddUser(newUser);
        }

        public bool AddUser(UserCredentials user)
        {
            UserCredentials newUser = new UserCredentials();

            try
            {
                //If the user id or password is invalid, user error messages are put to list and exit
                if (!ScrubValidate.isValidUserIdLength(user.UserId)) { return false; }
                if (ScrubValidate.hasBadCharacters(user.UserId)) { return false; }
                if (!isValidPassword(user.Password)) { return false; }

                
                string ValidUserName = user.UserId;
                string HashedPassword = string.Empty;
                if(this.UserErrors != null) { UserErrors.Clear(); }

                HashedPassword = SecurityHelper.HashPassword(user.Password, Config.SaltLength, Config.HashIterations, Config.nHash);

                newUser.UserId = user.UserId;
                newUser.Password = HashedPassword;

                DataStoreCommands.AddUser(newUser);

                return true;
            }
            catch (Exception ex)
            {
                //Log Error
                var error = $"AccountManager.AddUser -  User: {user.UserId} PWD: {user.Password} ErrorMessage: {ex.Message}";
                return false;
            }
        }


        public bool isValidPassword(string password)
        {
            bool isValid = true;

            if (!ScrubValidate.isValidPassword(password))
            {
                this.UserErrors = ScrubValidate.GetPasswordErrors();
                isValid = false;
            }

            return isValid;
        }

        public bool isValidUserIdLength(string UserId)
        {
            if (!ScrubValidate.isValidUserIdLength(UserId)) { return false; }

            return true;
        }

        public bool PopulateWithRecords()
        {
            try
            {
                UserCredentials user1 = new UserCredentials();
                user1.UserId = "JohnDoe";
                user1.Password = "Password0!";
                this.AddUser(user1);

                UserCredentials user2 = new UserCredentials();
                user2.UserId = "Morpheous";
                user2.Password = "P@ssword!2";
                this.AddUser(user2);

                UserCredentials user3 = new UserCredentials();
                user3.UserId = "Trinity";
                user3.Password = "Th3M@tr!x";
                this.AddUser(user3);

                return true;
            }
            catch (Exception ex)
            {
                //ToDo : Log Error
                var error = ex.ToString();
                return false;
            }
        }

        public void LogOut(string UserId)
        {
            if (this.isValidPassword(UserId))
            {
                //ToDo : Check if logged in
                DataStoreCommands.SessionRemove(UserId);
            }
            else
            {
                //log this internal error
            }
        }

    }
}
