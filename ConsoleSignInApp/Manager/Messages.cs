using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSignInApp.Manager
{
    public class Messages
    {
        public static void StartOptions()
        {
            Console.WriteLine("Welcome. Please select an option. (enter option numnber");
            Console.WriteLine("1. Log In");
            Console.WriteLine("2. Register");
            Console.WriteLine("e. Exit");
        }

        public static void LoggedInOptions()
        {
            Console.WriteLine("Welcome. Please select an option. (enter option character");
            Console.WriteLine("a. Log Out");
            Console.WriteLine("b. Register");
            //Console.WriteLine("c. Update");
            Console.WriteLine("e. Exit");
        }

    }
}
