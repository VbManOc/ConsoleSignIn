using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Services;
using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Manager
{
    public class Main
    {
        UserCredentials user;
        AccountManager accountManager;
        AuthenticationService authenticationService;
        string UserIdLocal = string.Empty;

        public Main()
        {
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

            //Populate the datastore
            accountManager.PopulateWithRecords();
        }

        public void ShowStartOptions()
        {
            Messages.StartOptions();
        }

        public void OptionsToRun(string OptionEntryValue)
        {
            int OptionNumber = 0;
            string OptionalChar = string.Empty;

            bool isNumeric = int.TryParse(OptionEntryValue, out OptionNumber);

            //Logged In Actions : Char Option Values
            if (!isNumeric)
            {
                LoggedInActions(OptionEntryValue.ToLower().Trim());
            }
            else //Not Logged In Actions : Numeric Option Values
            {
                LogInActions(OptionNumber);
            }
        }
        public void LogInActions(int OptionEntryNumber)
        {
            //Console.WriteLine("1. Log In");
            //Console.WriteLine("2. Register");
            //Console.WriteLine("e. Exit");
            switch (OptionEntryNumber)
            {
                case 1://LogIn
                    LogUserIn();
                    break;
                case 2:
                    RegisterUser();
                    break;
                default:
                    Console.WriteLine("Select From Options");
                    break;

            }
        }
        public void LoggedInActions(string OptionEntryNumber)
        {
            //Console.WriteLine("Welcome. Please select an option. (enter option character");
            //Console.WriteLine("a. Log Out");
            //Console.WriteLine("b. Register");
            switch (OptionEntryNumber)
            {
                case "a":
                    LogUserOut();
                    break;
                case "b":
                    Console.WriteLine("You must log out to Register new user.");
                    break;
                default:
                    Console.WriteLine("Select From Options");
                    break;

            }
        }

        public void LogUserIn()
        {
            bool success = false;

            Console.WriteLine("Please Enter A User Id");
            string UserId = Console.ReadLine();

            Console.WriteLine("Please Enter A Password");
            string Password = Console.ReadLine();

            success =  this.authenticationService.LogInUser(UserId, Password);
            if (success)
            {
                UserIdLocal = UserId;
                Console.WriteLine("Logged In");
                Messages.LoggedInOptions();
            }
            else
            {
                Console.WriteLine("Failed Try Again");
                LogUserIn();
            }

        }

        public void LogUserOut()
        {
            accountManager.LogOut(UserIdLocal);
            Messages.StartOptions();
        }

        public void RegisterUser()
        {
            bool success = false;

            Console.WriteLine("Please Enter A User Id");
            string UserId = Console.ReadLine();

            Console.WriteLine("Please Enter A Password");
            string Password = Console.ReadLine();

            success = accountManager.UserAdd(UserId, Password);
            if (success)
            {
                UserIdLocal = UserId;
                Console.WriteLine("User Added & Logged In");
                Messages.LoggedInOptions();
            }
            else
            {
                Console.WriteLine("Failed Try Again");
                LogUserIn();
            }
        }
    }
}
