using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Infrastructure
{
    public class DataStore
    {
        static DataStore instance;
        public Dictionary<string, string> Repo = new Dictionary<string, string>();
        public Dictionary<string, string> Sessions = new Dictionary<string, string>();


        private DataStore()
        {
            // There is no spoon - Neo
        }
        public static DataStore Instance()
        {
            //Note: Not thread safe singleton BUT not the intent of this project
            if (instance == null)
            {
                instance = new DataStore();
            }
            return instance;
        }

    }
}