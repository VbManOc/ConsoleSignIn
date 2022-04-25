using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Infrastructure;
using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Services
{
    public class AuthenticationService
    {
        ISecurityHelper SecurityHelper;
        IDataStoreQueries DataStoreQueries;
        IScrubValidate ScrubValidate;

        private List<string> UserErrors;

        public List<string> ErrorMessages
        {
            get
            {
                return this.UserErrors;
            }
        }

        public AuthenticationService(ISecurityHelper securityHelper, IScrubValidate scrubValidate, IDataStoreQueries dataStoreQueries)
        {
            this.SecurityHelper = securityHelper;
            this.ScrubValidate = scrubValidate;
            this.DataStoreQueries = dataStoreQueries;
        }

        public bool LogInUser(string UserId, string UserPassword)
        {
            UserCredentials user = new UserCredentials();
            user.UserId = UserId;
            user.Password = UserPassword;

            return AuthenticateUser(user);
        }

        public bool AuthenticateUser(UserCredentials user)
        {
            UserCredentials userCredentials = new UserCredentials();

            if (!isValidUserId(user.UserId))
            { return false; }

            if (!isValidPassword(user.Password))
            { return false; }

            userCredentials.UserId = user.UserId;
            userCredentials.Password = SecurityHelper.HashPassword(user.Password, Config.SaltLength, Config.HashIterations, Config.nHash);

            return DataStoreQueries.ValidateUser(userCredentials);
        }

        private bool isValidUserId(string UserId)
        {
            if (!ScrubValidate.isValidUserIdLength(UserId))
            {
                UserErrors.Add("Invalid User Name");
                return false;
            }
            if (ScrubValidate.hasBadCharacters(UserId))
            {
                UserErrors.Add("Invalid User Name");
                return false;
            }
            return true;
        }

        private bool isValidPassword(string Password)
        {
            if (!ScrubValidate.isValidPasswordLength(Password))
            {
                UserErrors.Add("Invalid Password");
                return false;
            }

            if (!ScrubValidate.isValidPasswordFormat(Password))
            {
                UserErrors.Add("Invalid Password");
                return false;
            }
            if (ScrubValidate.hasBadCharacters(Password))
            {
                UserErrors.Add("Invalid Password");
                return false;
            }
            if (!ScrubValidate.isValidPasswordFormat(Password))
            {
                UserErrors.Add("Invalid Password");
                return false;
            }

            return true;
        }
    }
}
