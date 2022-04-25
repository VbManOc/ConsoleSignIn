using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Entities;

namespace ConsoleSignInApp.Infrastructure
{
    public class DataStoreQueries : IDataStoreQueries
    {
        DataStore db;

        public DataStoreQueries()
        {
            db = DataStore.Instance();
        }

        public bool ValidateUser(UserCredentials user)
        {
            bool result = false;
            try
            {
                string userValue = $"{user.UserId} : {user.Password}";
                result = db.Repo.ContainsValue(userValue);
                return result;
            }
            catch (Exception ex)
            {
                //Log Errors
                string error = $"DataStoreQueries.ValidateUser UserId:{user.UserId} Password{user.Password} Exception: {ex.Message}";
                return false;
            }
        }

    }
}
