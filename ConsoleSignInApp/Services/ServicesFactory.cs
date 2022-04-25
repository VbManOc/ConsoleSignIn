using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Infrastructure;

namespace ConsoleSignInApp.Services
{
    public class ServicesFactory
    {
        public static ISecurityHelper GetSecurityHelper()
        {
            return SecurityHelper.Instance();
        }

        public static IScrubValidate GetScrubValidate()
        {
            return new ScrubValidate();
        }

        public static IDataStoreCommands GetDataStoreCommands()
        {
            return new DataStoreCommands();

        }

        public static IDataStoreQueries GetDataStoreQueries()
        {
            return new DataStoreQueries();
        }

    }
}
